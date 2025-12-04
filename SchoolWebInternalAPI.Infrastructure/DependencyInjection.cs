using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SchoolWebInternalAPI.Application.Interfaces;
using SchoolWebInternalAPI.Infrastructure.Data;
using SchoolWebInternalAPI.Infrastructure.Repositories;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Infrastructure.Repositories.Pages;

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
            services.AddScoped<IHomePageRepository, HomePageRepository>();
            services.AddScoped<IContactPageRepository, ContactPageRepository>();
            services.AddScoped<IFooterPageRepository, FooterPageRepository>();


            return services;
        }
    }
}
