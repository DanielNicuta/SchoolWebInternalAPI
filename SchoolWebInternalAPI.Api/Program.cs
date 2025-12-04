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
