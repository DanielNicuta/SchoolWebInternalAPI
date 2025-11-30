var builder = WebApplication.CreateBuilder(args);

// Add services to container

builder.Services.AddControllers();

// (Later) Add other dependencies: DbContext, Repos, Services, AutoMapper, etc.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
