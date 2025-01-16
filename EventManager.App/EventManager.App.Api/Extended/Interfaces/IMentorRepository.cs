using EventManager.App.Api.Extended.Models;

namespace EventManager.App.Api.Extended.Interfaces;

public interface IMentorshipRepository
{
    /// <summary>
    /// Get mentorship by id.
    /// </summary>
    /// <param name="rowKey">Post identity.</param>
    /// <returns></returns>
    MentorshipEntity GetMentorship(string rowKey);

    /// <summary>
    /// Get all mentorships.
    /// </summary>
    /// <returns></returns>
    List<MentorshipEntity> GetMentorships();

    /// <summary>
    /// Create mentorship.
    /// </summary>
    /// <param name="mentorshipEntity">mentorship entity.</param>
    /// <returns></returns>
    bool CreateMentorship(MentorshipEntity mentorshipEntity);

    /// <summary>
    /// Update mentorship.
    /// </summary>
    /// <param name="mentorshipEntity">mentorship entity.</param>
    /// <returns></returns>
    bool UpdateMentorship(MentorshipEntity mentorshipEntity);

    /// <summary>
    /// Delete mentorship.
    /// </summary>
    /// <param name="partitionKey">Post partition key.</param>
    /// <param name="rowKey">Post row key.</param>
    /// <returns></returns>
    bool DeleteMentorship(string partitionKey, string rowKey);
}
