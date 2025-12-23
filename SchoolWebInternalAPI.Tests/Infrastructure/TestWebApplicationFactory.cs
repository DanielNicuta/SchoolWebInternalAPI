using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolWebInternalAPI.Infrastructure.Data;

namespace SchoolWebInternalAPI.Tests.Infrastructure;

public sealed class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    private SqliteConnection? _connection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureAppConfiguration((context, config) =>
        {
            // Provide JWT settings so AuthService doesn't crash with null values
            var testConfig = new Dictionary<string, string?>
            {
                ["Jwt:Key"] = "THIS_IS_A_TEST_KEY_CHANGE_ME_12345678901234567890",
                ["Jwt:Issuer"] = "SchoolWebInternalAPI",
                ["Jwt:Audience"] = "SchoolWebInternalAPI",
                ["Jwt:ExpireMinutes"] = "60",
                ["ConnectionStrings:DefaultConnection"] = "DataSource=:memory:"
            };

            config.AddInMemoryCollection(testConfig);
        });

        builder.ConfigureServices(services =>
        {
            // Remove existing DbContext registration (the real one)
            var dbContextDescriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(DbContextOptions<SchoolDbContext>));

            if (dbContextDescriptor != null)
                services.Remove(dbContextDescriptor);

            // Keep the SQLite connection open for the entire test run
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            services.AddDbContext<SchoolDbContext>(options =>
            {
                options.UseSqlite(_connection);
            });

            // Build service provider and create schema
            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SchoolDbContext>();
            db.Database.EnsureCreated();
        });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            _connection?.Dispose();
        }
    }
}
