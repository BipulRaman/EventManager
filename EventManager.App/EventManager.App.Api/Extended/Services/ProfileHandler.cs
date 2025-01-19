using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Interfaces;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Basic.Utilities;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using System.Net;

namespace EventManager.App.Api.Extended.Services;

public class ProfileHandler : IProfileHandler
{
    private readonly IProfileRepository profileRepository;
    private readonly IEmailService emailService;
    private readonly IProfilePhotoRepository profilePhotoRepository;
    private readonly ILogger<ProfileHandler> logger;

    public ProfileHandler(IProfileRepository profileService, IEmailService emailService, IProfilePhotoRepository profilePhotoRepository, ILogger<ProfileHandler> logger)
    {
        profileRepository = profileService;
        this.emailService = emailService;
        this.profilePhotoRepository = profilePhotoRepository;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public OpResult<ProfileData> GetUserProfile(HttpContext httpContext)
    {
        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(GetUserProfile)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<ProfileData> opResult = new OpResult<ProfileData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            ProfileData profileData = profileRepository.GetUserDetailsById(ContextHelper.GetLoggedInUser(httpContext)?.Id);
            profileData.Photo = GetProfilePhoto(profileData.Id);
            opResult.Result = profileData;
            opResult.Status = HttpStatusCode.OK;
            opResult.ErrorCode = ErrorCode.None;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(ProfileHandler)}.{nameof(GetUserProfile)} => Error occurred while fetching user profile for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }

        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(GetUserProfile)} => Method completed for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<ProfileDataPublic> GetUserProfilePublic(string id)
    {
        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(GetUserProfilePublic)} => Method started for {id} ");
        OpResult<ProfileDataPublic> opResult = new OpResult<ProfileDataPublic>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                ProfileEntity profileEntity = profileRepository.GetUserDetailsById(id);
                profileEntity.Photo = GetProfilePhoto(id);
                opResult.Result = (ProfileDataPublic)profileEntity;
                opResult.Status = HttpStatusCode.OK;
                opResult.ErrorCode = ErrorCode.None;
            }
            else
            {
                opResult.Status = HttpStatusCode.BadRequest;
                opResult.ErrorCode = ErrorCode.Common_BadRequest;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(ProfileHandler)}.{nameof(GetUserProfilePublic)} => Error occurred while fetching user profile for {id} ");
        }

        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(GetUserProfilePublic)} => Method completed for {id} ");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<ProfileData> AddUserProfile(ProfileDataCreate profileCreateData)
    {
        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(AddUserProfile)} => Method started for {profileCreateData.Email}.");
        OpResult<ProfileData> opResult = new OpResult<ProfileData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (profileCreateData.IsValid())
            {
                if (!profileRepository.CheckUserExists(profileCreateData.Email))
                {
                    ProfileEntity profileEntity = (ProfileEntity)profileCreateData;
                    bool isUserAdded = profileRepository.AddUser(profileEntity);

                    if (isUserAdded)
                    {
                        opResult.ErrorCode = ErrorCode.None;
                        opResult.Status = HttpStatusCode.Created;
                        opResult.Result = (ProfileData)profileEntity;
                    }
                    else
                    {
                        opResult.ErrorCode = ErrorCode.Entity_Create_Failed;
                        opResult.Status = HttpStatusCode.InternalServerError;
                    }
                }
                else
                {
                    opResult.Status = HttpStatusCode.Conflict;
                    opResult.ErrorCode = ErrorCode.Entity_AlreadyExist;
                }

            }
            else
            {
                opResult.ErrorCode = ErrorCode.Common_BadRequest;
                opResult.Status = HttpStatusCode.BadRequest;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(ProfileHandler)}.{nameof(AddUserProfile)} => Error occurred while adding user profile for {profileCreateData.Email}.");
        }

        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(AddUserProfile)} => Method completed for {profileCreateData.Email}.");
        return opResult;
    }
    /// <inheritdoc/>
    public OpResult<ProfileData> UpdateUserProfile(HttpContext httpContext, ProfileData userData)
    {
        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(UpdateUserProfile)} => Method started for {userData.Email}.");
        OpResult<ProfileData> opResult = new OpResult<ProfileData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            User contextUserInfo = (User)httpContext.Items[NameConstants.USER_KEY];
            if (contextUserInfo.Roles.Contains(nameof(Role.InvitedUser)))
            {
                contextUserInfo.Roles = contextUserInfo.Roles.Replace(nameof(Role.InvitedUser), nameof(Role.User));
            }

            if (userData.IsValidToUpdate())
            {
                if (contextUserInfo.Id.Equals(userData.Id))
                {
                    ProfileEntity profileEntity = userData.ConvertToUpdateEntity(httpContext);
                    bool isUserUpdated = profileRepository.UpdateUserDetails(profileEntity);

                    if (isUserUpdated)
                    {
                        opResult.ErrorCode = ErrorCode.None;
                        opResult.Status = HttpStatusCode.OK;
                        opResult.Result = (ProfileData)profileEntity;
                        opResult.Result.Photo = GetProfilePhoto(userData.Id);
                    }
                    else
                    {
                        opResult.ErrorCode = ErrorCode.Entity_Update_Failed;
                        opResult.Status = HttpStatusCode.InternalServerError;
                    }
                }
                else
                {
                    opResult.ErrorCode = ErrorCode.Entity_Mismatched;
                    opResult.Status = HttpStatusCode.Unauthorized;
                }
            }
            else
            {
                opResult.ErrorCode = ErrorCode.Common_BadRequest;
                opResult.Status = HttpStatusCode.BadRequest;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(ProfileHandler)}.{nameof(UpdateUserProfile)} => Error occurred while updating user profile for {userData.Email}.");
            throw;
        }

        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(UpdateUserProfile)} => Method completed for {userData.Email}.");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<bool> UpdateProfilePhoto(HttpContext httpContext, IFormFile file)
    {
        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(UpdateProfilePhoto)} => Method started for {httpContext.User.Identity.Name}.");
        OpResult<bool> opResult = new OpResult<bool>()
        {
            Result = false,
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (file != null && file.ContentType.Contains("image") && file.Length < 1000000)
            {
                User contextUserInfo = (User)httpContext.Items[NameConstants.USER_KEY];
                bool isProfilePhotoUploaded = profilePhotoRepository.UploadProfilePhoto(file, $"{contextUserInfo.Id}.jpeg");
                if (isProfilePhotoUploaded)
                {
                    opResult.ErrorCode = ErrorCode.None;
                    opResult.Status = HttpStatusCode.OK;
                    opResult.Result = true;
                }
                else
                {
                    opResult.ErrorCode = ErrorCode.Entity_Update_Failed;
                    opResult.Status = HttpStatusCode.InternalServerError;
                }
            }
            else
            {
                opResult.ErrorCode = ErrorCode.Common_BadRequest;
                opResult.Status = HttpStatusCode.BadRequest;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(ProfileHandler)}.{nameof(UpdateProfilePhoto)} => Error occurred while updating profile photo for {httpContext.User.Identity.Name}.");
        }

        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(UpdateProfilePhoto)} => Method completed for {httpContext.User.Identity.Name}.");
        return opResult;
    }

    private string GetProfilePhoto(string id)
    {
        string imageUrl = $"{id}.jpeg";
        string imageBase64 = profilePhotoRepository.GetProfilePhotoBase64Src(imageUrl);
        return imageBase64;
    }

    public OpResult<List<ProfileDataPublic>> GetPeopleNearby(HttpContext httpContext, int radiusInKm)
    {
        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(GetPeopleNearby)} => Method started.");
        var opResult = new OpResult<List<ProfileDataPublic>>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            User contextUserInfo = (User)httpContext.Items[NameConstants.USER_KEY];
            ProfileData currentProfileData = profileRepository.GetUserDetailsById(contextUserInfo.Id);
            if (currentProfileData != null && currentProfileData.Latitude != 0 && currentProfileData.Longitude != 0)
            {
                (double minLat, double maxLat, double minLon, double maxLon) = GetCoordinateRange(currentProfileData.Latitude, currentProfileData.Longitude, radiusInKm);
                List<ProfileEntity> profileEntities = profileRepository.GetUsersInGeoRange(minLat, maxLat, minLon, maxLon);
                profileEntities = profileEntities.OrderByDescending(x => x.Timestamp).ToList();

                List<ProfileDataPublic> profileDataPublics = new List<ProfileDataPublic>();
                foreach (var profileEntity in profileEntities)
                {
                    ProfileDataPublic profileDataPublic = (ProfileDataPublic)profileEntity;
                    if (profileDataPublic.Id != currentProfileData.Id)
                    {
                        //profileDataPublic.Photo = GetProfilePhoto(profileEntity.RowKey);
                        profileDataPublics.Add(profileDataPublic);
                    }
                }

                opResult.ErrorCode = ErrorCode.None;
                opResult.Status = HttpStatusCode.OK;
                opResult.Result = profileDataPublics;

            }
            else
            {
                logger.LogWarning($"{nameof(ProfileHandler)}.{nameof(GetPeopleNearby)} => User not found.");
                opResult.ErrorCode = ErrorCode.Entity_NotFound;
                opResult.Status = HttpStatusCode.NotFound;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(ProfileHandler)}.{nameof(GetPeopleNearby)} => Error occurred while getting the people nearby.");
            opResult.ErrorCode = ErrorCode.Common_InternalServerError;
            opResult.Status = HttpStatusCode.InternalServerError;
        }

        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(GetPeopleNearby)} => Method ended.");

        return opResult;
    }

    public OpResult<List<ProfileDataPublic>> GetPeopleByPhone(string phone)
    {
        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(GetPeopleByPhone)} => Method started.");
        OpResult<List<ProfileDataPublic>> opResult = new OpResult<List<ProfileDataPublic>>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            List<ProfileEntity> profileEntities = profileRepository.GetUsersByPhone(phone);
            List<ProfileDataPublic> profileDataPublics = new List<ProfileDataPublic>();
            foreach (var profileEntity in profileEntities)
            {
                ProfileDataPublic profileDataPublic = (ProfileDataPublic)profileEntity;
                //profileDataPublic.Photo = GetProfilePhoto(profileEntity.RowKey);
                profileDataPublics.Add(profileDataPublic);
            }

            opResult.ErrorCode = ErrorCode.None;
            opResult.Status = HttpStatusCode.OK;
            opResult.Result = profileDataPublics;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(ProfileHandler)}.{nameof(GetPeopleByPhone)} => Error occurred while getting the people by phone.");
        }

        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(GetPeopleByPhone)} => Method ended.");

        return opResult;
    }

    public OpResult<ProfileData> UpdateGeoCoordinates(HttpContext httpContext, double latitude, double longitude)
    {
        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(UpdateGeoCoordinates)} => Method started.");
        OpResult<ProfileData> opResult = new OpResult<ProfileData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            User contextUserInfo = (User)httpContext.Items[NameConstants.USER_KEY];
            ProfileData currentUserProfileData = profileRepository.GetUserDetailsById(contextUserInfo.Id);
            if (currentUserProfileData != null)
            {
                currentUserProfileData.Latitude = latitude;
                currentUserProfileData.Longitude = longitude;
                ProfileEntity profileEntity = currentUserProfileData.ConvertToUpdateEntity(httpContext);
                bool isUserUpdated = profileRepository.UpdateUserDetails(profileEntity);

                if (isUserUpdated)
                {
                    opResult.ErrorCode = ErrorCode.None;
                    opResult.Status = HttpStatusCode.OK;
                    opResult.Result = (ProfileData)profileEntity;
                    opResult.Result.Photo = GetProfilePhoto(currentUserProfileData.Id);
                }
                else
                {
                    logger.LogWarning($"{nameof(ProfileHandler)}.{nameof(UpdateGeoCoordinates)} => Failed to update user details.");
                    opResult.ErrorCode = ErrorCode.Entity_Update_Failed;
                    opResult.Status = HttpStatusCode.InternalServerError;
                }
            }
            else
            {
                logger.LogWarning($"{nameof(ProfileHandler)}.{nameof(UpdateGeoCoordinates)} => User not found.");
                opResult.ErrorCode = ErrorCode.Entity_NotFound;
                opResult.Status = HttpStatusCode.NotFound;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(ProfileHandler)}.{nameof(UpdateGeoCoordinates)} => Error updating geo coordinates.");
            opResult.ErrorCode = ErrorCode.Common_InternalServerError;
            opResult.Status = HttpStatusCode.InternalServerError;
        }

        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(UpdateGeoCoordinates)} => Method ended.");
        return opResult;
    }

    public OpResult<bool> VenueCheckIn(string userId)
    {
        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(VenueCheckIn)} => Method started.");
        OpResult<bool> opResult = new OpResult<bool>()
        {
            Result = false,
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            bool response = profileRepository.VenueCheckIn(userId);
            if (response)
            {
                opResult.ErrorCode = ErrorCode.None;
                opResult.Status = HttpStatusCode.OK;
                opResult.Result = true;
            }
            else
            {
                logger.LogWarning($"{nameof(ProfileHandler)}.{nameof(VenueCheckIn)} => Failed to update venue check-in.");
                opResult.ErrorCode = ErrorCode.Entity_Update_Failed;
                opResult.Status = HttpStatusCode.InternalServerError;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(ProfileHandler)}.{nameof(VenueCheckIn)} => Error updating venue check-in.");
            opResult.ErrorCode = ErrorCode.Common_InternalServerError;
            opResult.Status = HttpStatusCode.InternalServerError;
        }

        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(VenueCheckIn)} => Method ended.");

        return opResult;
    }

    public OpResult<bool> GiftCheckIn(string userId)
    {
        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(GiftCheckIn)} => Method started.");
        OpResult<bool> opResult = new OpResult<bool>()
        {
            Result = false,
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            bool response = profileRepository.GiftCheckIn(userId);
            if (response)
            {
                opResult.ErrorCode = ErrorCode.None;
                opResult.Status = HttpStatusCode.OK;
                opResult.Result = true;
            }
            else
            {
                logger.LogWarning($"{nameof(ProfileHandler)}.{nameof(GiftCheckIn)} => Failed to update gift check-in.");
                opResult.ErrorCode = ErrorCode.Entity_Update_Failed;
                opResult.Status = HttpStatusCode.InternalServerError;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(ProfileHandler)}.{nameof(GiftCheckIn)} => Error updating gift check-in.");
            opResult.ErrorCode = ErrorCode.Common_InternalServerError;
            opResult.Status = HttpStatusCode.InternalServerError;
        }

        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(GiftCheckIn)} => Method ended.");

        return opResult;
    }

    public OpResult<bool> MealCheckIn(string userId)
    {
        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(MealCheckIn)} => Method started.");
        OpResult<bool> opResult = new OpResult<bool>()
        {
            Result = false,
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            bool response = profileRepository.MealCheckIn(userId);
            if (response)
            {
                opResult.ErrorCode = ErrorCode.None;
                opResult.Status = HttpStatusCode.OK;
                opResult.Result = true;
            }
            else
            {
                logger.LogWarning($"{nameof(ProfileHandler)}.{nameof(MealCheckIn)} => Failed to update meal check-in.");
                opResult.ErrorCode = ErrorCode.Entity_Update_Failed;
                opResult.Status = HttpStatusCode.InternalServerError;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(ProfileHandler)}.{nameof(MealCheckIn)} => Error updating meal check-in.");
            opResult.ErrorCode = ErrorCode.Common_InternalServerError;
            opResult.Status = HttpStatusCode.InternalServerError;
        }

        logger.LogInformation($"{nameof(ProfileHandler)}.{nameof(MealCheckIn)} => Method ended.");

        return opResult;
    }

    private static (double minLat, double maxLat, double minLon, double maxLon) GetCoordinateRange(double latitude, double longitude, double distance)
    {
        var radius = 6371; // Radius of the Earth in kilometers
        var deltaLat = distance / radius * (180 / Math.PI);
        var deltaLon = distance / (radius * Math.Cos(Math.PI * latitude / 180)) * (180 / Math.PI);
        var minLat = latitude - deltaLat;
        var maxLat = latitude + deltaLat;
        var minLon = longitude - deltaLon;
        var maxLon = longitude + deltaLon;
        return (minLat, maxLat, minLon, maxLon);
    }
}
