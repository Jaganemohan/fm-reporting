using Microsoft.AspNetCore.Mvc;
using ReportingAPI.Models.DTOs;
using ReportingAPI.Services.Interfaces;

namespace ReportingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPreferenceController : ControllerBase
    {
        private readonly IUserPreferenceService _userPreferenceService;
        private readonly ILogger<UserPreferenceController> _logger;

        public UserPreferenceController(IUserPreferenceService userPreferenceService, ILogger<UserPreferenceController> logger)
        {
            _userPreferenceService = userPreferenceService;
            _logger = logger;
        }

        /// <summary>
        /// Gets user preferences for a specific report and user
        /// </summary>
        /// <param name="reportId">ID of the report</param>
        /// <param name="userId">ID of the user</param>
        /// <returns>User preferences for the specified report and user</returns>
        [HttpGet("{reportId}/{userId}")]
        public async Task<IActionResult> GetUserPreference(int reportId, int userId)
        {
            try
            {
                _logger.LogInformation("Getting user preference for ReportId: {ReportId}, UserId: {UserId}", reportId, userId);

                var preference = await _userPreferenceService.GetUserPreferenceAsync(reportId, userId);

                if (preference == null)
                {
                    _logger.LogInformation("No preference found for ReportId: {ReportId}, UserId: {UserId}", reportId, userId);
                    return NotFound($"No preference found for report {reportId} and user {userId}");
                }

                return Ok(preference);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user preference for ReportId: {ReportId}, UserId: {UserId}", reportId, userId);
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        /// <summary>
        /// Saves or updates user preferences for a report
        /// </summary>
        /// <param name="preference">User preference data to save</param>
        /// <returns>Saved user preference</returns>
        [HttpPost]
        public async Task<IActionResult> SaveUserPreference([FromBody] SaveUserPreferenceDto preference)
        {
            try
            {
                _logger.LogInformation("Saving user preference for ReportId: {ReportId}, UserId: {UserId}",
                    preference.ReportId, preference.UserId);

                var savedPreference = await _userPreferenceService.SaveUserPreferenceAsync(preference);

                if (savedPreference == null)
                {
                    _logger.LogWarning("Failed to save preference - Report with ID {ReportId} not found", preference.ReportId);
                    return NotFound($"Report with ID {preference.ReportId} not found");
                }

                return Ok(savedPreference);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving user preference for ReportId: {ReportId}, UserId: {UserId}",
                    preference.ReportId, preference.UserId);
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
}
