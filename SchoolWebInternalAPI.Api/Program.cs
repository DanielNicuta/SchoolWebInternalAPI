using FluentValidation;
using FluentValidation.AspNetCore;
using SchoolWebInternalAPI.Application.Validators.Teachers;
using Microsoft.EntityFrameworkCore;
using SchoolWebInternalAPI.Application.Interfaces;
using SchoolWebInternalAPI.Application.Services;
using SchoolWebInternalAPI.Infrastructure.Data;
using SchoolWebInternalAPI.Infrastructure.Repositories;
using SchoolWebInternalAPI.Application.DTOs.Teachers;
using SchoolWebInternalAPI.API.Middlewares;
using SchoolWebInternalAPI.Application.Mapping;
using SchoolWebInternalAPI.Application.DTOs.Pages.Contact;
using SchoolWebInternalAPI.Application.Validators.Pages.Contact;
using SchoolWebInternalAPI.Application.DTOs.Pages.Footer;
using SchoolWebInternalAPI.Application.Validators.Pages.Footer;
using SchoolWebInternalAPI.Application.DTOs.Pages.History;
using SchoolWebInternalAPI.Application.Validators.Pages.History;
using SchoolWebInternalAPI.Application.DTOs.Pages.Home;
using SchoolWebInternalAPI.Application.Validators.Pages.Home;
using SchoolWebInternalAPI.Application.DTOs.Pages.Links;
using SchoolWebInternalAPI.Application.Validators.Pages.Links;
using SchoolWebInternalAPI.Application.DTOs.Pages.Mission;
using SchoolWebInternalAPI.Application.Validators.Pages.Mission;
using SchoolWebInternalAPI.Application.DTOs.Pages.Organization;
using SchoolWebInternalAPI.Application.Validators.Pages.Organization;
using SchoolWebInternalAPI.Application.DTOs.Pages.Settings;
using SchoolWebInternalAPI.Application.Validators.Pages.Settings;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------------------------------
//  Add DbContext
// ------------------------------------------------------
builder.Services.AddDbContext<SchoolDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// You can replace UseSqlite with your DB engine (SqlServer, MySql etc.)

// ------------------------------------------------------
//  Dependency Injection (Application + Infrastructure)
// ------------------------------------------------------
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// ------------------------------------------------------
//  Add Controllers
// ------------------------------------------------------
builder.Services.AddControllers();

// ------------------------------------------------------
//  Swagger (Development Only)
// ------------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ------------------------------------------------------
// AutoMapper
// ------------------------------------------------------
builder.Services.AddAutoMapper(typeof(TeacherProfile));
builder.Services.AddAutoMapper(typeof(PagesProfile));

// ------------------------------------------------------
// FluentValidation
// ------------------------------------------------------
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
builder.Services.AddFluentValidationAutoValidation();



var app = builder.Build();

// ------------------------------------------------------
//  Configure Pipeline
// ------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();