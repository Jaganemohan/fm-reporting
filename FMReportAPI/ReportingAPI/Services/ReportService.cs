using Microsoft.EntityFrameworkCore;
using ReportingAPI.Data;
using ReportingAPI.Models.DTOs;
using ReportingAPI.Services.Interfaces;

namespace ReportingAPI.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserPreferenceService _userPreferenceService;

        public ReportService(ApplicationDbContext context, IUserPreferenceService userPreferenceService)
        {
            _context = context;
            _userPreferenceService = userPreferenceService;
        }

        public async Task<ReportDetailDto?> GetReportDetailAsync(int reportId, int userId)
        {
            var report = await _context.Reports
                .Include(r => r.Parameters)
                .FirstOrDefaultAsync(r => r.Id == reportId);

            if (report == null)
                return null;

            var userPreference = await _userPreferenceService.GetUserPreferenceAsync(reportId, userId);

            var reportDetailDto = new ReportDetailDto
            {
                ReportId = report.Id,
                ReportName = report.Name,
                ReportType = report.ReportType,
                ApiUrl = report.ApiUrl,
                MethodType = report.MethodType,
                Description = report.Description,
                Parameters = report.Parameters.Select(p => new ReportParameterDto
                {
                    Id = p.Id,
                    ParameterName = p.ParameterName,
                    DisplayName = p.DisplayName,
                    ParameterType = p.ParameterType,
                    IsRequired = p.IsRequired,
                    DefaultValue = p.DefaultValue,
                    Description = p.Description,
                    DisplayOrder = p.DisplayOrder
                }).OrderBy(p => p.DisplayOrder).ToList(),
                UserPreference = userPreference
            };

            return reportDetailDto;
        }

        public async Task<IEnumerable<ReportDetailDto?>> GetAllReportsAsync()
        {
            var reports = await _context.Reports
                .AsNoTracking().ToListAsync();

            if (reports == null)
                return null;

            List<ReportDetailDto> reportDetailDtos = new List<ReportDetailDto>();
            foreach (var report in reports)
            {
                var reportDetailDto = new ReportDetailDto
                {
                    ReportId = report.Id,
                    ReportName = report.Name,
                    ReportType = report.ReportType,
                    ApiUrl = report.ApiUrl,
                    MethodType = report.MethodType,
                    Description = report.Description,                    
                };
                reportDetailDtos.Add(reportDetailDto);
            }

            return reportDetailDtos;
        }
    }
}
