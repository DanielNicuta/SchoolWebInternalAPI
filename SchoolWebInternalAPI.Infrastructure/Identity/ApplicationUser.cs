using Microsoft.AspNetCore.Identity;

namespace SchoolWebInternalAPI.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        // Optional extra fields for later
        public string? FullName { get; set; }
    }
}
