using COJ.Web.Infrastructure.Environment;
using COJ.Web.Infrestructure.Data;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using COJ.Web.Domain.Attributes;
using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Values;
using COJ.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

AppEnvironment.LoadEnvFile();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthorization((options) => { RegisterPolicies(options); });
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

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(Locales.DefaultLocale)
    .AddSupportedCultures(Locales.SupportedLocales)
    .AddSupportedUICultures(Locales.SupportedLocales);
    
localizationOptions.ApplyCurrentCultureToResponseHeaders = true;

app.UseRequestLocalization(localizationOptions);

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

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
}

void RegisterServices()
{
    var domainAssembly = Assembly.Load("COJ.Web.Domain");
    var infrastructureAssembly = Assembly.Load("COJ.Web.Infrastructure");

    builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), infrastructureAssembly);
    builder.Services.AddSingleton<AppEnvironment>();

    foreach (var ti in domainAssembly.GetTypes().Where(x => x.IsInterface && x.IsPublic && x.Name.Contains("Service")))
    {
        var serviceImplementation = infrastructureAssembly.GetTypes()
            .SingleOrDefault(x => x.IsClass && x.IsPublic && ti.IsAssignableFrom(x));
        if (serviceImplementation == null)
            Console.WriteLine("Warning: Too many implementations for the same service abstract interface");
        else
        {
            if (serviceImplementation.IsDefined(typeof(InjectAsSingletonAttribute), false))
                builder.Services.AddSingleton(ti, serviceImplementation);
            else
                builder.Services.AddTransient(ti, serviceImplementation);
        }
    }
}

void RegisterPolicies(AuthorizationOptions options)
{
    options.AddPolicy(AccountPermissions.CreateProblem,
        policyBuilder => policyBuilder.RequirePermission(AccountPermissions.CreateProblem));
    options.AddPolicy(AccountPermissions.DeleteProblem,
        policyBuilder => policyBuilder.RequirePermission(AccountPermissions.DeleteProblem));
    options.AddPolicy(AccountPermissions.UpdateProblem,
        policyBuilder => policyBuilder.RequirePermission(AccountPermissions.UpdateProblem));
}

public partial class Program
{
}