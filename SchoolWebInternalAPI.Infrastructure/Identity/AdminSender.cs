using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolWebInternalAPI.Domain.Entities;
using SchoolWebInternalAPI.Domain.Entities.Pages;
using SchoolWebInternalAPI.Domain.Entities.PagesCSM;
using SchoolWebInternalAPI.Infrastructure.Data;

namespace SchoolWebInternalAPI.Infrastructure.Identity;

public static class AdminSeeder
{

    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var db = scope.ServiceProvider.GetRequiredService<SchoolDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var seed = config.GetSection("AdminSeed").Get<AdminSeedSettings>() ?? new AdminSeedSettings();

        // 1) Ensure DB created + migrations applied
        await db.Database.MigrateAsync();

        // 2) Ensure Admin role exists
        if (!await roleManager.RoleExistsAsync(seed.RoleName))
        {
            await roleManager.CreateAsync(new IdentityRole(seed.RoleName));
        }

        // 3) Ensure Admin user exists + has Admin role
        var admin = await userManager.FindByEmailAsync(seed.Email);
        if (admin == null)
        {
            admin = new ApplicationUser
            {
                UserName = seed.Email,
                Email = seed.Email,
                EmailConfirmed = true,
                FullName = seed.FullName
            };

            var createResult = await userManager.CreateAsync(admin, seed.Password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join("; ", createResult.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to create admin user: {errors}");
            }
        }

        if (!await userManager.IsInRoleAsync(admin, seed.RoleName))
        {
            await userManager.AddToRoleAsync(admin, seed.RoleName);
        }

        await SeedAsync(db);
    }

    private static async Task SeedAsync(SchoolDbContext db)
    {
        if (!await db.HomePages.AnyAsync())
        {
            db.HomePages.Add(new HomePage
            {
                HeroTitle = "Welcome to our School",
                HeroSubtitle = "Learning today, leading tomorrow",
                HeroButtonText = "Learn More",
                HeroButtonUrl = "/about",
                HeroImageUrl = "/assets/img/hero-bg.jpg",

                AboutTitle = "About our school",
                AboutSubtitle = "A short story about us",
                AboutHtml = "<p>Replace this text from Admin Panel.</p>",
                AboutImageUrl = "/assets/img/about.jpg",

                Highlight1Title = "Quality Education",
                Highlight1Text = "Replace from Admin Panel.",
                Highlight1Icon = "bi-mortarboard",

                Highlight2Title = "Modern Facilities",
                Highlight2Text = "Replace from Admin Panel.",
                Highlight2Icon = "bi-building",

                Highlight3Title = "Community",
                Highlight3Text = "Replace from Admin Panel.",
                Highlight3Icon = "bi-people",

                SeoTitle = "Home",
                SeoDescription = "School home page",
                OgImageUrl = "/assets/img/hero-bg.jpg",

                IsPublished = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        if (!await db.ContactPages.AnyAsync())
        {
            db.ContactPages.Add(new ContactPage
            {
                Title = "Contact",
                Subtitle = "Get in touch",
                Address = "Your address here",
                Phone = "+40 000 000 000",
                Email = "contact@schoolweb.local",
                MapEmbedUrl = null,
                InfoHtml = "<p>Replace this contact info from Admin Panel.</p>",

                SeoTitle = "Contact",
                SeoDescription = "Contact page",
                OgImageUrl = null,

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        if (!await db.HistoryPages.AnyAsync())
        {
            db.HistoryPages.Add(new HistoryPage
            {
                Title = "History",
                Subtitle = "Our story",
                ContentHtml = "<p>Replace history content from Admin Panel.</p>",
                SideImageUrl = "/assets/img/about.jpg",

                SeoTitle = "History",
                SeoDescription = "School history",
                OgImageUrl = null,

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        if (!await db.LinksPages.AnyAsync())
        {
            db.LinksPages.Add(new LinksPage
            {
                Title = "Useful Links",
                IntroHtml = "<p>Replace intro from Admin Panel.</p>",
                LinksHtml = "<ul><li><a href=\"#\">Example link</a></li></ul>",

                SeoTitle = "Links",
                SeoDescription = "Useful links",
                OgImageUrl = null,

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        if (!await db.MissionPages.AnyAsync())
        {
            db.MissionPages.Add(new MissionPage
            {
                Title = "Mission",
                IntroHtml = "<p>Replace intro from Admin Panel.</p>",
                MissionHtml = "<p>Replace mission content from Admin Panel.</p>",
                ImageUrl = "/assets/img/about.jpg",

                SeoTitle = "Mission",
                SeoDescription = "School mission",
                OgImageUrl = null,

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        if (!await db.OrganizationPages.AnyAsync())
        {
            db.OrganizationPages.Add(new OrganizationPage
            {
                Title = "Organization",
                DescriptionHtml = "<p>Replace description from Admin Panel.</p>",
                OrgChartImageUrl = null,
                OrgChartFileUrl = null,

                SeoTitle = "Organization",
                SeoDescription = "School organization",
                OgImageUrl = null,

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        if (!await db.SiteSettings.AnyAsync())
        {
            db.SiteSettings.Add(new SiteSettings
            {
                SiteName = "School Website",
                LogoUrl = "/assets/img/logo.png",
                FaviconUrl = "/assets/img/favicon.png",
                DefaultLanguage = "en",

                ContactEmail = "contact@schoolweb.local",
                ContactPhone = "+40 000 000 000",
                Address = "Your address here",

                FacebookUrl = null,
                YoutubeUrl = null,
                TwitterUrl = null,

                CookieBannerText = "This website uses cookies to improve your experience.",

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        if (!await db.FooterContents.AnyAsync())
        {
            db.FooterContents.Add(new FooterContent
            {
                FooterText = "© School Website. All rights reserved.",
                UsefulLinksJson = "[]",
                SocialLinksJson = "[]",
                NewsletterTitle = "Newsletter",
                NewsletterText = "Subscribe for updates.",

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        await db.SaveChangesAsync();
    }
}
