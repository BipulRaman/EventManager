using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Extended.Models;

namespace EventManager.App.Api.Extended.Interfaces;

public interface IProfileHandler
{
    /// <summary>
    /// Get the user profile.
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public OpResult<ProfileData> GetUserProfile(HttpContext httpContext);

    /// <summary>
    /// Get the public user profile.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public OpResult<ProfileDataPublic> GetUserProfilePublic(string userId);

    /// <summary>
    /// Add the user profile.
    /// </summary>
    /// <param name="rawProfileData"></param>
    /// <returns></returns>
    public OpResult<ProfileData> AddUserProfile(ProfileDataCreate rawProfileData);

    /// <summary>
    /// Update the user profile.
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="profileData"></param>
    /// <returns></returns>
    public OpResult<ProfileData> UpdateUserProfile(HttpContext httpContext, ProfileData profileData);

    /// <summary>
    /// Update the user profile photo.
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    public OpResult<bool> UpdateProfilePhoto(HttpContext httpContext, IFormFile file);

    /// <summary>
    /// Updates the geo coordinates of the current user.
    /// </summary>
    /// param name="httpContext"></param>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    /// <returns></returns>
    OpResult<ProfileData> UpdateGeoCoordinates(HttpContext httpContext, double latitude, double longitude);

    /// <summary>
    /// Gets the people nearby.
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="radiusInKm"></param>
    /// <returns></returns>
    OpResult<List<ProfileDataPublic>> GetPeopleNearby(HttpContext httpContext, int radiusInKm);
}
