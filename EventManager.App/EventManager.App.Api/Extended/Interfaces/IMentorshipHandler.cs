using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Extended.Models;

namespace EventManager.App.Api.Extended.Interfaces;

public interface IMentorshipHandler
{
    /// <summary>
    /// Get all mentorships.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <returns></returns>
    OpResult<List<MentorshipData>> GetMentorships(HttpContext httpContext);

    /// <summary>
    /// Get mentorship by id.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="mentorshipId">Mentorship identity</param>
    /// <returns></returns>
    OpResult<MentorshipData> GetMentorship(HttpContext httpContext, string mentorshipId);

    /// <summary>
    /// Create mentorship.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="rawMentorshipData">Raw mentorship creation data.</param>
    /// <returns></returns>
    OpResult<MentorshipData> CreateMentorship(HttpContext httpContext, MentorshipData rawMentorshipData);

    /// <summary>
    /// Update mentorship.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="mentorshipData">Mentorship data.</param>
    /// <returns></returns>
    OpResult<MentorshipData> UpdateMentorship(HttpContext httpContext, MentorshipData mentorshipData);

    /// <summary>
    /// Delete mentorship.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="mentorshipId">Mentorship entity.</param>
    /// <returns></returns>
    OpResult<bool> DeleteMentorship(HttpContext httpContext, string mentorshipId);
}
