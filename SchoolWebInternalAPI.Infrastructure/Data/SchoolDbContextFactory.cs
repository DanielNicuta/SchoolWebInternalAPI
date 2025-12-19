using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SchoolWebInternalAPI.Infrastructure.Data
{
    public class SchoolDbContextFactory : IDesignTimeDbContextFactory<SchoolDbContext>
    {
        public SchoolDbContext CreateDbContext(string[] args)
        {
            // Load configuration manually for design-time tools
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SchoolDbContext>();

            optionsBuilder.UseSqlite(config.GetConnectionString("DefaultConnection"));

            return new SchoolDbContext(optionsBuilder.Options);
        }
    }
}
