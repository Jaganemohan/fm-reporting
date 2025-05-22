namespace ReportingAPI.Models.DTOs
{
    public class ReportParameterDto
    {
        public int Id { get; set; }
        public string ParameterName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string ParameterType { get; set; } = string.Empty;
        public bool IsRequired { get; set; }
        public string? DefaultValue { get; set; }
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
    }
}
