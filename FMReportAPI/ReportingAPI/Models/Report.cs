using System.ComponentModel.DataAnnotations;

namespace ReportingAPI.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string ReportType { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string ApiUrl { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string MethodType { get; set; } = string.Empty; // GET or POST

        [StringLength(500)]
        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation property
        public virtual ICollection<ReportParameter> Parameters { get; set; } = new List<ReportParameter>();
    }
}
