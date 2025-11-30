namespace SchoolWebInternalAPI.Application.DTOs
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName  { get; set; } = string.Empty;
        public string? Title    { get; set; }
        public string? Bio      { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
