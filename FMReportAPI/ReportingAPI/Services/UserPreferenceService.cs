using ReportingAPI.Data;
using ReportingAPI.Models.DTOs;
using ReportingAPI.Models;
using ReportingAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ReportingAPI.Services
{
    public class UserPreferenceService : IUserPreferenceService
    {
        private readonly ApplicationDbContext _context;

        public UserPreferenceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserPreferenceDto?> GetUserPreferenceAsync(int reportId, int userId)
        {
            var preference = await _context.UserPreferences
                .FirstOrDefaultAsync(up => up.ReportId == reportId && up.UserId == userId);

            if (preference == null)
                return null;

            return new UserPreferenceDto
            {
                Id = preference.Id,
                ReportId = preference.ReportId,
                UserId = preference.UserId,
                PreferenceData = preference.PreferenceData,
                DefaultView = preference.DefaultView
            };
        }

        public async Task<UserPreferenceDto?> SaveUserPreferenceAsync(SaveUserPreferenceDto preferenceDto)
        {
            // Check if report exists
            var reportExists = await _context.Reports.AnyAsync(r => r.Id == preferenceDto.ReportId);
            if (!reportExists)
                return null;

            // Check if preference already exists
            var existingPreference = await _context.UserPreferences
                .FirstOrDefaultAsync(up => up.ReportId == preferenceDto.ReportId && up.UserId == preferenceDto.UserId);

            UserPreference preference;

            if (existingPreference != null)
            {
                // Update existing preference
                existingPreference.PreferenceData = preferenceDto.PreferenceData;
                existingPreference.DefaultView = preferenceDto.DefaultView;
                existingPreference.UpdatedDate = DateTime.UtcNow;
                preference = existingPreference;
            }
            else
            {
                // Create new preference
                preference = new UserPreference
                {
                    ReportId = preferenceDto.ReportId,
                    UserId = preferenceDto.UserId,
                    PreferenceData = preferenceDto.PreferenceData,
                    DefaultView = preferenceDto.DefaultView,
                    CreatedDate = DateTime.UtcNow
                };
                _context.UserPreferences.Add(preference);
            }

            await _context.SaveChangesAsync();

            return new UserPreferenceDto
            {
                Id = preference.Id,
                ReportId = preference.ReportId,
                UserId = preference.UserId,
                PreferenceData = preference.PreferenceData,
                DefaultView = preference.DefaultView
            };
        }
    }
}
