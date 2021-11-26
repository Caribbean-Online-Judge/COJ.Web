using COJ.Web.API;
using COJ.Web.Domain.Abstract;
using COJ.Web.Infraestructure;
using COJ.Web.Infraestructure.Services;
using COJ.Web.Infrestructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

AppEnvironment.LoadEnvFile();

builder.Configuration.AddEnvironmentVariables();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

AddSwagger();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly(),
    Assembly.Load("COJ.Web.Infraestructure"));

builder.Services.AddSingleton<AppEnvironment>();

var host = builder.Configuration.GetValue<string>(AppEnvironment.DATABASE_HOST_KEY);
var username = builder.Configuration.GetValue<string>("DATABASE_USERNAME");
var password = builder.Configuration.GetValue<string>("DATABASE_PASSWORD");
var name = builder.Configuration.GetValue<string>("DATABASE_NAME");
var connection = $"Host={host};Username={username};Password={password};Database={name}";

builder.Services.AddDbContext<MainDbContext>(options =>
               options.UseNpgsql(connection, b => b.MigrationsAssembly("COJ.Web.API")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

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
    builder.Services.AddTransient<IHashService, HashService>();
    builder.Services.AddTransient<IAuthService, AuthService>();
    builder.Services.AddTransient<IEmailService, EmailService>();
}