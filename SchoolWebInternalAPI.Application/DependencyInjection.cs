using Microsoft.Extensions.DependencyInjection;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
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

            return services;
        }
    }
}
