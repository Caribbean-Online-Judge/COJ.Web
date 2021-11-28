using COJ.Web.Infraestructure.Environment;
using COJ.Web.Infrestructure.Data;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

AppEnvironment.LoadEnvFile();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        //ValidateIssuerSigningKey = false,
        //ValidIssuer = builder.Configuration["Jwt:Issuer"],
        //ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT_SECRET"]))
    };
});

AddSwagger();
RegisterDatabase();
RegisterServices();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void RegisterDatabase()
{
    var host = builder.Configuration.GetValue<string>(AppEnvironment.DATABASE_HOST_KEY);
    var username = builder.Configuration.GetValue<string>("DATABASE_USERNAME");
    var password = builder.Configuration.GetValue<string>("DATABASE_PASSWORD");
    var name = builder.Configuration.GetValue<string>("DATABASE_NAME");
    var connection = $"Host={host};Username={username};Password={password};Database={name}";

    builder.Services.AddDbContext<MainDbContext>(options =>
                   options.UseNpgsql(connection, b => b.MigrationsAssembly("COJ.Web.API")));
}

void AddSwagger()
{
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Caribbean Online Judge",
        });

        // using System.Reflection;
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
}

void RegisterServices()
{
    var domainAssembly = Assembly.Load("COJ.Web.Domain");
    var infraestructureAssembly = Assembly.Load("COJ.Web.Infraestructure");


    builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), infraestructureAssembly);

    builder.Services.AddSingleton<AppEnvironment>();

    foreach (Type ti in domainAssembly.GetTypes().Where(x => x.IsInterface && x.IsPublic && x.Name.Contains("Service")))
    {
        var serviceImplementation = infraestructureAssembly.GetTypes().SingleOrDefault(x => x.IsClass && x.IsPublic && ti.IsAssignableFrom(x));
        if (serviceImplementation == null)
            Console.WriteLine("Warning: Too many implementations for the same service abstract interface");
        else
        {
            builder.Services.AddTransient(ti, serviceImplementation);
        }
    }
}