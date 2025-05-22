using Microsoft.AspNetCore.Mvc;
using ReportingAPI.Services.Interfaces;

namespace ReportingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController(IReportService reportService, ILogger<ReportController> logger) : ControllerBase
    {
        private readonly IReportService _reportService = reportService;
        private readonly ILogger<ReportController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            try
            {
                _logger.LogInformation("Getting all reports");
                var reports = await _reportService.GetAllReportsAsync();
                if (reports == null || !reports.Any())
                {
                    _logger.LogWarning("No reports found");
                    return NotFound("No reports found");
                }
                return Ok(reports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all reports");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        /// <summary>
        /// Gets detailed information about a report including parameters and user preferences
        /// </summary>
        /// <param name="reportId">ID of the report to retrieve</param>
        /// <param name="userId">ID of the user to retrieve preferences for</param>
        /// <returns>Report details with parameters and user preferences</returns>
        [HttpGet("{reportId}/{userId}")]
        public async Task<IActionResult> GetReportDetail(int reportId, int userId)
        {
            try
            {
                _logger.LogInformation("Getting report detail for ReportId: {ReportId}, UserId: {UserId}", reportId, userId);

                var reportDetail = await _reportService.GetReportDetailAsync(reportId, userId);

                if (reportDetail == null)
                {
                    _logger.LogWarning("Report not found with ID: {ReportId}", reportId);
                    return NotFound($"Report with ID {reportId} not found");
                }

                return Ok(reportDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting report detail for ReportId: {ReportId}, UserId: {UserId}", reportId, userId);
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
}
