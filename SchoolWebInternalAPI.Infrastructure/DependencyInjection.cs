using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolWebInternalAPI.Application.Interfaces;
using SchoolWebInternalAPI.Application.Interfaces.Auth;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Infrastructure.Identity;
using SchoolWebInternalAPI.Infrastructure.Repositories;
using SchoolWebInternalAPI.Infrastructure.Repositories.Pages;

namespace SchoolWebInternalAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // -------------------------------
            // Repositories
            // -------------------------------
            services.AddScoped<ITeacherRepository, TeacherRepository>();

            services.AddScoped<IHomePageRepository, HomePageRepository>();
            services.AddScoped<IContactPageRepository, ContactPageRepository>();
            services.AddScoped<IFooterContentRepository, FooterContentRepository>();
            services.AddScoped<IHistoryPageRepository, HistoryPageRepository>();
            services.AddScoped<ILinksPageRepository, LinksPageRepository>();
            services.AddScoped<IMissionPageRepository, MissionPageRepository>();
            services.AddScoped<IOrganizationPageRepository, OrganizationPageRepository>();
            services.AddScoped<ISiteSettingsRepository, SiteSettingsRepository>();

            // -------------------------------
            // Auth Implementation
            // -------------------------------
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
