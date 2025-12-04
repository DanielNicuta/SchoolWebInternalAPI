using Microsoft.Extensions.DependencyInjection;
using SchoolWebInternalAPI.Application.Interfaces;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Application.Services;
using SchoolWebInternalAPI.Application.Services.Pages;

namespace SchoolWebInternalAPI.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register application services here later
            services.AddScoped<IHomePageService, HomePageService>();
            services.AddScoped<IContactPageService, ContactPageService>();
            services.AddScoped<IFooterContentService, FooterContentService>();
            services.AddScoped<IHistoryPageService, HistoryPageService>();
            services.AddScoped<ILinksPageService, LinksPageService>();
            services.AddScoped<IMissionPageService, MissionPageService>();
            services.AddScoped<IOrganizationPageService, OrganizationPageService>();
            services.AddScoped<ISiteSettingsService, SiteSettingsService>();
            services.AddScoped<ITeacherService, TeacherService>();


            return services;
        }
    }
}
