using Microsoft.EntityFrameworkCore;
using SchoolWebInternalAPI.Application.Interfaces;
using SchoolWebInternalAPI.Application.Services;
using SchoolWebInternalAPI.Infrastructure.Data;
using SchoolWebInternalAPI.Infrastructure.Repositories;

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

// ------------------------------------------------------
//  Add Controllers
// ------------------------------------------------------
builder.Services.AddControllers();

// ------------------------------------------------------
//  Swagger (Development Only)
// ------------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ------------------------------------------------------
//  Configure Pipeline
// ------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
