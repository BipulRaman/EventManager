using EventManager.App.Api.Extended.Models;

namespace EventManager.App.Api.Extended.Interfaces;

public interface IProfileRepository
{
    /// <summary>
    /// Get user details from the database using email.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    ProfileEntity GetUserDetailsByEmail(string email);

    /// <summary>
    /// Get user details from the database using id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ProfileEntity GetUserDetailsById(string id);

    /// <summary>
    /// Get users in the geo range.
    /// </summary>
    /// <param name="minLat"></param>
    /// <param name="maxLat"></param>
    /// <param name="minLon"></param>
    /// <param name="maxLon"></param>
    /// <returns></returns>
    List<ProfileEntity> GetUsersInGeoRange(double minLat, double maxLat, double minLon, double maxLon);

    /// <summary>
    /// Add User to the database.
    /// </summary>
    /// <param name="userInfo"></param>
    bool AddUser(ProfileEntity userInfo);

    /// <summary>
    /// Add/Update user details in the database.
    /// </summary>
    /// <param name="profileEntity"></param>
    bool UpdateUserDetails(ProfileEntity profileEntity);

    /// <summary>
    /// Delete user from the database.
    /// </summary>
    /// <param name="rowKey"></param>
    bool DeleteUser(string rowKey);

    /// <summary>
    /// Check if user exists in the database.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    bool CheckUserExists(string email);
}
