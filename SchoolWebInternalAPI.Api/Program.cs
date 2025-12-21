using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolWebInternalAPI.API.Middlewares;
using SchoolWebInternalAPI.Application;
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
using SchoolWebInternalAPI.Application.Validators.Pages.Contact;
using SchoolWebInternalAPI.Application.Validators.Pages.Footer;
using SchoolWebInternalAPI.Application.Validators.Pages.History;
using SchoolWebInternalAPI.Application.Validators.Pages.Home;
using SchoolWebInternalAPI.Application.Validators.Pages.Links;
using SchoolWebInternalAPI.Application.Validators.Pages.Mission;
using SchoolWebInternalAPI.Application.Validators.Pages.Organization;
using SchoolWebInternalAPI.Application.Validators.Pages.Settings;
using SchoolWebInternalAPI.Application.Validators.Teachers;
using SchoolWebInternalAPI.Domain.Entities; // ApplicationUser
using SchoolWebInternalAPI.Infrastructure;
using SchoolWebInternalAPI.Infrastructure.Data;
using SchoolWebInternalAPI.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------
// DB Context (make sure AddInfrastructure does NOT also add this again)
// --------------------------------------------
builder.Services.AddDbContext<SchoolDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// --------------------------------------------
// Application + Infrastructure registration
// --------------------------------------------
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// --------------------------------------------
// Identity (use ApplicationUser, NOT IdentityUser)
// --------------------------------------------
builder.Services
    .AddIdentityCore<ApplicationUser>(options =>
    {
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SchoolDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

// --------------------------------------------
// JWT Settings bind
// --------------------------------------------
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSection["Key"];

if (string.IsNullOrWhiteSpace(jwtKey))
    throw new InvalidOperationException("JWT Key missing. Add Jwt:Key to appsettings.json");

// --------------------------------------------
// Authentication (ONLY ONCE!)
// --------------------------------------------
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
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });

builder.Services.AddAuthorization();

// --------------------------------------------
// AutoMapper (one registration is enough, but this is OK)
// --------------------------------------------
builder.Services.AddAutoMapper(typeof(TeacherProfile).Assembly);
builder.Services.AddAutoMapper(typeof(PagesProfile).Assembly);

// --------------------------------------------
// FluentValidation
// --------------------------------------------
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

// --------------------------------------------
// Controllers + Swagger
// --------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --------------------------------------------
// Pipeline
// --------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Optional admin seed
using (var scope = app.Services.CreateScope())
{
    await AdminSeeder.SeedAsync(scope.ServiceProvider);
}

app.Run();

public partial class Program { }

