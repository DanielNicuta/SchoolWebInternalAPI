using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SchoolWebInternalAPI.Application.Interfaces;
using SchoolWebInternalAPI.Infrastructure.Data;
using SchoolWebInternalAPI.Infrastructure.Repositories;

namespace SchoolWebInternalAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                                   ?? "Data Source=school.db";

            services.AddDbContext<SchoolDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddScoped<ITeacherRepository, TeacherRepository>();

            return services;
        }
    }
}
