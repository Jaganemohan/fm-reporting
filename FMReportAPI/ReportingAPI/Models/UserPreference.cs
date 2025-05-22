using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReportingAPI.Models
{
    public class UserPreference
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ReportId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string PreferenceData { get; set; } = string.Empty; // JSON data

        [StringLength(100)]
        public string? DefaultView { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        // Navigation property
        [ForeignKey("ReportId")]
        public virtual Report Report { get; set; } = null!;
    }
}
