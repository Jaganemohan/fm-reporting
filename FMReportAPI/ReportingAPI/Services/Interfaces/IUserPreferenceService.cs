using ReportingAPI.Models.DTOs;

namespace ReportingAPI.Services.Interfaces
{
    public interface IUserPreferenceService
    {
        Task<UserPreferenceDto?> GetUserPreferenceAsync(int reportId, int userId);
        Task<UserPreferenceDto?> SaveUserPreferenceAsync(SaveUserPreferenceDto preference);
    }
}
