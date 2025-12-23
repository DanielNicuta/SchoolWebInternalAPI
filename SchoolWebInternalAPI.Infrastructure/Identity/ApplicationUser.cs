using Microsoft.AspNetCore.Identity;

namespace SchoolWebInternalAPI.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        // Optional extra fields for later
        public string? FullName { get; set; }
    }
}
