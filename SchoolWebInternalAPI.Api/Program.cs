using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolWebInternalAPI.API.Middlewares;
using SchoolWebInternalAPI.Application;
using SchoolWebInternalAPI.Application.DTOs.Auth;
using SchoolWebInternalAPI.Application.DTOs.Pages.Contact;
using SchoolWebInternalAPI.Application.DTOs.Pages.Footer;
using SchoolWebInternalAPI.Application.DTOs.Pages.History;
using SchoolWebInternalAPI.Application.DTOs.Pages.Home;
using SchoolWebInternalAPI.Application.DTOs.Pages.Links;
using SchoolWebInternalAPI.Application.DTOs.Pages.Mission;
using SchoolWebInternalAPI.Application.DTOs.Pages.Organization;
using SchoolWebInternalAPI.Application.DTOs.Pages.Settings;
using SchoolWebInternalAPI.Application.DTOs.Teachers;
using SchoolWebInternalAPI.Application.Mapping;
using SchoolWebInternalAPI.Application.Validators.Auth;
using SchoolWebInternalAPI.Application.Validators.Pages.Contact;
using SchoolWebInternalAPI.Application.Validators.Pages.Footer;
using SchoolWebInternalAPI.Application.Validators.Pages.History;
using SchoolWebInternalAPI.Application.Validators.Pages.Home;
using SchoolWebInternalAPI.Application.Validators.Pages.Links;
using SchoolWebInternalAPI.Application.Validators.Pages.Mission;
using SchoolWebInternalAPI.Application.Validators.Pages.Organization;
using SchoolWebInternalAPI.Application.Validators.Pages.Settings;
using SchoolWebInternalAPI.Application.Validators.Teachers;
using SchoolWebInternalAPI.Infrastructure;
using SchoolWebInternalAPI.Infrastructure.Data;
using SchoolWebInternalAPI.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);


// ------------------------------------------------------------
// Application + Infrastructure
// ------------------------------------------------------------
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// ------------------------------------------------------------
// Health checks (App + DB)
// Requires: Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore
// ------------------------------------------------------------
builder.Services.AddHealthChecks()
    .AddDbContextCheck<SchoolDbContext>(
        name: "school",
        failureStatus: HealthStatus.Unhealthy,
        tags: new[] { "ready" });

// ------------------------------------------------------------
// JWT Settings + Auth
// ------------------------------------------------------------
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSection["Key"];

if (string.IsNullOrWhiteSpace(jwtKey))
    throw new InvalidOperationException("JWT Key missing. Add Jwt:Key to appsettings.json");

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew = TimeSpan.FromSeconds(30),
            RoleClaimType = ClaimTypes.Role
        };
    });

builder.Services.AddAuthorization();

// ------------------------------------------------------------
// AutoMapper
// ------------------------------------------------------------
builder.Services.AddAutoMapper(typeof(TeacherProfile).Assembly);
builder.Services.AddAutoMapper(typeof(PagesProfile).Assembly);

// ------------------------------------------------------------
// FluentValidation
// ------------------------------------------------------------
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddScoped<IValidator<TeacherCreateDto>, TeacherCreateDtoValidator>();
builder.Services.AddScoped<IValidator<TeacherUpdateDto>, TeacherUpdateDtoValidator>();

builder.Services.AddScoped<IValidator<ContactPageUpdateDto>, ContactPageUpdateDtoValidator>();
builder.Services.AddScoped<IValidator<FooterContentUpdateDto>, FooterContentUpdateDtoValidator>();
builder.Services.AddScoped<IValidator<HistoryPageUpdateDto>, HistoryPageUpdateDtoValidator>();
builder.Services.AddScoped<IValidator<HomePageUpdateDto>, HomePageUpdateDtoValidator>();
builder.Services.AddScoped<IValidator<LinksPageUpdateDto>, LinksPageUpdateDtoValidator>();
builder.Services.AddScoped<IValidator<MissionPageUpdateDto>, MissionPageUpdateDtoValidator>();
builder.Services.AddScoped<IValidator<OrganizationPageUpdateDto>, OrganizationPageUpdateDtoValidator>();
builder.Services.AddScoped<IValidator<SiteSettingsUpdateDto>, SiteSettingsUpdateDtoValidator>();

builder.Services.AddScoped<IValidator<LoginDto>, LoginRequestDtoValidator>();
builder.Services.AddScoped<IValidator<RegisterDto>, RegisterRequestDtoValidator>();
builder.Services.AddScoped<IValidator<RevokeRequestDto>, RevokeRequestDtoValidator>();
builder.Services.AddScoped<IValidator<LogoutRequestDto>, LogoutRequestDtoValidator>();

builder.Services.Configure<RefreshTokenCleanupOptions>(
    builder.Configuration.GetSection("RefreshTokenCleanup"));

// ------------------------------------------------------------
// Controllers + Swagger
// ------------------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SchoolWebInternalAPI",
        Version = "v1",
        Description = "Internal API for SchoolWebsite CMS + Admin features"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Paste ONLY the token here (without 'Bearer ')."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ------------------------------------------------------------
// Rate Limiting
// ------------------------------------------------------------
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
    {
        var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: ip,
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 200,
                Window = TimeSpan.FromMinutes(1),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            });
    });

    options.AddPolicy("login", httpContext =>
    {
        var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        return RateLimitPartition.GetFixedWindowLimiter(ip, _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 10,
            Window = TimeSpan.FromMinutes(1),
            QueueLimit = 0
        });
    });

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

var app = builder.Build();

// ------------------------------------------------------------
// Pipeline
// ------------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();

// ------------------------------------------------------------
// Health + Info endpoints
// ------------------------------------------------------------
app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";

        var result = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                error = e.Value.Exception?.Message
            }),
            totalDurationMs = report.TotalDuration.TotalMilliseconds
        };

        await context.Response.WriteAsJsonAsync(result);
    }
});

// Simple info endpoint (helps with deploy debugging)
app.MapGet("/info", () =>
{
    var commit = Environment.GetEnvironmentVariable("GIT_COMMIT") ?? "local";
    return Results.Ok(new
    {
        name = "SchoolWebInternalAPI",
        environment = app.Environment.EnvironmentName,
        commit,
        utc = DateTime.UtcNow
    });
});

app.MapControllers();

// ------------------------------------------------------------
// Seed Admin
// ------------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    await AdminSeeder.SeedAsync(scope.ServiceProvider);
}

app.Run();

public partial class Program { }
