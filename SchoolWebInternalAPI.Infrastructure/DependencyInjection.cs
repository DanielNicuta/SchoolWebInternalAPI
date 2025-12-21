using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SchoolWebInternalAPI.Application.Interfaces;
using SchoolWebInternalAPI.Application.Interfaces.Auth;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Application.Services;
using SchoolWebInternalAPI.Application.Services.Pages;
using SchoolWebInternalAPI.Domain.Entities;
using SchoolWebInternalAPI.Infrastructure.Data;
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
            // -----------------------
            // DbContext
            // -----------------------
            services.AddDbContext<SchoolDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            // -----------------------
            // Identity (registers IUserStore<ApplicationUser>)
            // -----------------------
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<SchoolDbContext>()   // ✅ THIS FIXES IUserStore error
            .AddSignInManager()
            .AddDefaultTokenProviders();

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
            // Application services
            // -------------------------------
            services.AddScoped<ITeacherService, TeacherService>();

            services.AddScoped<IHomePageService, HomePageService>();
            services.AddScoped<IContactPageService, ContactPageService>();
            services.AddScoped<IFooterContentService, FooterContentService>();
            services.AddScoped<IHistoryPageService, HistoryPageService>();
            services.AddScoped<ILinksPageService, LinksPageService>();
            services.AddScoped<IMissionPageService, MissionPageService>();
            services.AddScoped<IOrganizationPageService, OrganizationPageService>();
            services.AddScoped<ISiteSettingsService, SiteSettingsService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddIdentity<ApplicationUser, IdentityRole>();


            return services;
        }
    }
}
