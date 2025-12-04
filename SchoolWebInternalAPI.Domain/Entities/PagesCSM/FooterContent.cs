using System;

namespace SchoolWebInternalAPI.Domain.Entities.PagesCSM
{
    public class FooterContent : BaseEntity
    {
        public string? FooterText { get; set; }

        // Can store serialized JSON lists for quick v1 implementation
        public string? UsefulLinksJson { get; set; }  // e.g. [{"text":"Home","url":"/"}]
        public string? SocialLinksJson { get; set; }  // e.g. [{"network":"facebook","url":"..."}]

        public string? NewsletterTitle { get; set; }
        public string? NewsletterText { get; set; }
    }
}
