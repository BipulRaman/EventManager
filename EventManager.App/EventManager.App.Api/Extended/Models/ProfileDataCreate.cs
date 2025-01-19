using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Extended.Enums;
using EventManager.App.Api.Extended.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventManager.App.Api.Extended.Models;

public class ProfileDataCreate : ProfileData
{
    [JsonIgnore]
    public new string PrimarySchoolName { get; set; } = string.Empty;

    [JsonIgnore]
    public new string SecondarySchoolNames { get; set; } = string.Empty;

    [JsonIgnore]
    public new List<string> Roles { get; set; } = new List<string>();

    [JsonIgnore]
    public new string CreatedBy { get; set; } = string.Empty;

    [JsonIgnore]
    public new string CreatedByName { get; set; } = string.Empty;

    [JsonIgnore]
    public new DateTimeOffset? CreatedAt { get; set; }

    [JsonIgnore]
    public new DateTimeOffset? ModifiedAt { get; set; }

    [JsonIgnore]
    public new string ModifiedBy { get; set; } = string.Empty;

    /// <summary>
    /// Validate the UserInfo object.
    /// </summary>
    /// <returns></returns>
    public bool IsValid()
    {
        var emailAddressAttribute = new EmailAddressAttribute();
        return !string.IsNullOrEmpty(Email) && emailAddressAttribute.IsValid(Email)
            && !string.IsNullOrEmpty(Name)
            && !string.IsNullOrEmpty(Gender)
            && !string.IsNullOrEmpty(Phone)
            && !string.IsNullOrEmpty(Location)
            && PrimarySchoolId > 0
            && (EntryYear >= 1985 && EntryYear <= DateTime.Now.Year || EntryYear == 0)
            && (EntryClass >= 6 && EntryClass <= 12 || EntryClass == 0)
            && (ExitYear >= 1985 && ExitYear <= DateTime.Now.Year || ExitYear == 0)
            && (ExitClass >= 6 && ExitClass <= 12 || ExitClass == 0)
            && Enum.TryParse(typeof(ProfileType), ProfileType, out _);
    }

    public static implicit operator ProfileEntity(ProfileDataCreate profileData)
    {
        string newId = Guid.NewGuid().ToString();
        DateTime dateTime = DateTime.UtcNow;

        return new ProfileEntity()
        {
            RowKey = newId,
            Name = profileData.Name,
            Email = profileData.Email,
            ModifiedBy = newId,
            CreatedBy = newId,
            CreatedAt = new DateTimeOffset(dateTime),
            Timestamp = new DateTimeOffset(dateTime),
            Roles = nameof(Role.User),
            SecurityKey = Guid.NewGuid().ToString(),
            Gender = profileData.Gender,
            IsContactInfoVisible = profileData.IsContactInfoVisible,
            Phone = profileData.Phone,
            Location = profileData.Location,
            Latitude = profileData.Latitude,
            Longitude = profileData.Longitude,
            Photo = profileData.Photo,
            ProfileType = profileData.ProfileType,
            EntryYear = profileData.EntryYear,
            EntryClass = profileData.EntryClass,
            ExitYear = profileData.ExitYear,
            ExitClass = profileData.ExitClass,
            PrimarySchoolId = profileData.PrimarySchoolId,
            PrimarySchoolName = SchoolDataHelper.GetFullSchoolName(profileData.PrimarySchoolId),
            SecondorySchoolIds = profileData.SecondarySchoolIds.Any() ? string.Join(",", profileData.SecondarySchoolIds) : string.Empty,
            SecondarySchoolNames = profileData.SecondarySchoolIds.Any() ? SchoolDataHelper.GetFullSchoolsName(profileData.SecondarySchoolIds) : string.Empty,
            IsHigherEducationInfoVisible = profileData.IsHigherEducationInfoVisible,
            IsStudying = profileData.IsStudying,
            University = profileData.University,
            Degree = profileData.Degree,
            IsEmploymentInfoVisible = profileData.IsEmploymentInfoVisible,
            IsWorking = profileData.IsWorking,
            Organisation = profileData.Organization,
            JobTitle = profileData.JobTitle,
            VenueCheckInDateTime = profileData.VenueCheckInDateTime,
            GiftCheckInDateTime = profileData.GiftCheckInDateTime,
            MealCheckInDateTime = profileData.MealCheckInDateTime,
        };
    }
}
