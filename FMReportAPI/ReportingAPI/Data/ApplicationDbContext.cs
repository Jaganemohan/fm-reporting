using ReportingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ReportingAPI.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public required DbSet<Report> Reports { get; set; }
        public required DbSet<ReportParameter> ReportParameters { get; set; }
        public required DbSet<UserPreference> UserPreferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<ReportParameter>()
                .HasOne(p => p.Report)
                .WithMany(r => r.Parameters)
                .HasForeignKey(p => p.ReportId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserPreference>()
                .HasOne(up => up.Report)
                .WithMany()
                .HasForeignKey(up => up.ReportId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure composite unique index for UserPreference
            modelBuilder.Entity<UserPreference>()
                .HasIndex(up => new { up.ReportId, up.UserId })
                .IsUnique();
        }
    }
}
