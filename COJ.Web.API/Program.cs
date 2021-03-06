using COJ.Web.Infrastructure.Environment;
using COJ.Web.Infrestructure.Data;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using COJ.Web.Domain.Attributes;
using COJ.Web.Domain.Values;
using COJ.Web.Infrastructure.Extensions;
using COJ.Web.Infrastructure.Resolvers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using ILogger = Serilog.ILogger;
using Microsoft.AspNetCore.OData;

var builder = WebApplication.CreateBuilder(args);

// remove default logging providers
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(SetUpLogger());

AppEnvironment.LoadEnvFile();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});
builder.Services.AddControllers()
    .AddOData(options =>
{
    options.Filter().OrderBy().Count().SkipToken().Expand().SetMaxTop(100);
    options.EnableNoDollarQueryOptions = true;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthorization(RegisterPolicies);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateIssuer = builder.Configuration.GetValue<bool>(JwtSettings.VALIDATE_ISSUER_KEY),
            ValidateAudience = builder.Configuration.GetValue<bool>(JwtSettings.VALIDATE_AUDIENCE_KEY),
            ValidIssuers = builder.Configuration.GetValue<string[]>(JwtSettings.VALID_ISSUER_KEY),
            ValidAudiences = builder.Configuration.GetValue<string[]>(JwtSettings.VALID_AUDIENCE_KEY),
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT_SECRET"])),
        };
    });

AddSwagger();
RegisterDatabase();
RegisterServices();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssembly(Assembly.Load("COJ.Web.Infrastructure"));
    ValidatorOptions.Global.PropertyNameResolver = CamelCasePropertyNameResolver.ResolvePropertyName;
});

var app = builder.Build();

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(Locales.DefaultLocale)
    .AddSupportedCultures(Locales.SupportedLocales)
    .AddSupportedUICultures(Locales.SupportedLocales);

localizationOptions.ApplyCurrentCultureToResponseHeaders = true;

app.UseRequestLocalization(localizationOptions);

if (app.Environment.IsDevelopment())
{
    app.UseCors();
}
else if (app.Environment.IsStaging())
{
    app.UseCors();

    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
}

app.UseSwagger();

if (app.Configuration.GetValue("ENABLE_SWAGGER_UI", false))
    app.UseSwaggerUI();

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
    var titleSuffix = builder.Environment.IsProduction() ? string.Empty : "(Staging)";

    builder.Services.AddSwaggerGen(options =>
    {
        if (!builder.Environment.IsDevelopment())
            options.AddServer(new OpenApiServer
            {
                Url = builder.Configuration["HOST_URL"]
            });

        options.EnableAnnotations();
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = $"Caribbean Online Judge {titleSuffix}",
            Contact = new OpenApiContact
            {
                Email = "community@caribbeanonlinejudge.org"
            }
        });

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. \nExample: 'Bearer oahsdoahsodiah'",
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

ILogger SetUpLogger()
{
    var step1 = new LoggerConfiguration()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .Enrich
        .FromLogContext();

    step1.WriteTo
        .File(AppEnvironment.LogsFileName, flushToDiskInterval: TimeSpan.FromSeconds(1),
            rollingInterval: RollingInterval.Hour);

    if (builder.Environment.IsDevelopment())
        step1.WriteTo
            .Console(theme: AnsiConsoleTheme.Code);

    return Log.Logger = step1.CreateLogger();
}

// For compatibility with End2End Tests
public partial class Program
{
}