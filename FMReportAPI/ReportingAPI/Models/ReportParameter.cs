using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReportingAPI.Models
{
    public class ReportParameter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ReportId { get; set; }

        [Required]
        [StringLength(100)]
        public string ParameterName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string DisplayName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string ParameterType { get; set; } = string.Empty; // string, int, date, etc.

        public bool IsRequired { get; set; }

        public string? DefaultValue { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public int DisplayOrder { get; set; }

        // Navigation property
        [ForeignKey("ReportId")]
        public virtual Report Report { get; set; } = null!;
    }
}
