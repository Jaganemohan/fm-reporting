namespace ReportingAPI.Models.DTOs
{
    public class ReportDetailDto
    {
        public int ReportId { get; set; }
        public string ReportName { get; set; } = string.Empty;
        public string ReportType { get; set; } = string.Empty;
        public string ApiUrl { get; set; } = string.Empty;
        public string MethodType { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<ReportParameterDto> Parameters { get; set; } = new List<ReportParameterDto>();
        public UserPreferenceDto? UserPreference { get; set; }
    }
}
