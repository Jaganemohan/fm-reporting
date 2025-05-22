namespace ReportingAPI.Models.DTOs
{
    public class UserPreferenceDto
    {
        public int Id { get; set; }
        public int ReportId { get; set; }
        public int UserId { get; set; }
        public string PreferenceData { get; set; } = string.Empty; 
        public string? DefaultView { get; set; }
    }
}
