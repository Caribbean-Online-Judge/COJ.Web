using COJ.Web.API;
using COJ.Web.Domain.Abstract;
using COJ.Web.Infraestructure;
using COJ.Web.Infraestructure.Services;
using COJ.Web.Infrestructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

AppEnvironment.LoadEnvFile();

builder.Configuration.AddEnvironmentVariables();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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


void RegisterServices()
{
    builder.Services.AddTransient<IHashService, HashService>();
    builder.Services.AddTransient<IAuthService, AuthService>();
    builder.Services.AddTransient<IEmailService, EmailService>();
}