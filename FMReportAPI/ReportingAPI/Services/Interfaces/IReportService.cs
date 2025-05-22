using ReportingAPI.Models.DTOs;

namespace ReportingAPI.Services.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<ReportDetailDto?>> GetAllReportsAsync();
        Task<ReportDetailDto?> GetReportDetailAsync(int reportId, int userId);
    }
}
