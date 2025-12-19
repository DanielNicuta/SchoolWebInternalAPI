using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
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
using SchoolWebInternalAPI.Infrastructure;
using SchoolWebInternalAPI.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------
// Application + Infrastructure registration
// --------------------------------------------
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// --------------------------------------------
// Identity
// --------------------------------------------
builder.Services.AddIdentityCore<IdentityUser>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<SchoolWebInternalAPI.Infrastructure.Data.SchoolDbContext>()
.AddDefaultTokenProviders();

// Authentication + Authorization
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// --------------------------------------------
// AutoMapper
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

app.UseAuthentication();  // <-- REQUIRED
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    await AdminSeeder.SeedAsync(scope.ServiceProvider);
}


app.Run();
