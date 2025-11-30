namespace SchoolWebInternalAPI.Domain.Entities
{
    public class Teacher : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? Bio { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
