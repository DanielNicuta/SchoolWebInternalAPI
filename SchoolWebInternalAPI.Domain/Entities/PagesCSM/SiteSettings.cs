using System;

namespace SchoolWebInternalAPI.Domain.Entities.PagesCSM
{
    public class SiteSettings : BaseEntity
    {
        public string SiteName { get; set; } = "Școala Gimnazială Crețeni";
        public string? LogoUrl { get; set; }
        public string? FaviconUrl { get; set; }

        public string? DefaultLanguage { get; set; } = "ro";

        // Contact
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }

        // Social
        public string? FacebookUrl { get; set; }
        public string? YoutubeUrl { get; set; }
        public string? TwitterUrl { get; set; }

        // Cookie banner text etc.
        public string? CookieBannerText { get; set; }
    }
}
