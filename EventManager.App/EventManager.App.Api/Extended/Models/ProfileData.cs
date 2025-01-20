using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Extended.Enums;
using EventManager.App.Api.Extended.Utilities;
using System.Text.Json.Serialization;

namespace EventManager.App.Api.Extended.Models;

public class ProfileData : BaseData
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [JsonPropertyName("isContactInfoVisible")]
    public bool IsContactInfoVisible { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("location")]
    public string Location { get; set; }

    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("photo")]
    public string Photo { get; set; }

    [JsonPropertyName("profileType")]
    public string ProfileType { get; set; }

    [JsonPropertyName("entryYear")]
    public int EntryYear { get; set; }

    [JsonPropertyName("entryClass")]
    public int EntryClass { get; set; }

    [JsonPropertyName("exitYear")]
    public int ExitYear { get; set; }

    [JsonPropertyName("exitClass")]
    public int ExitClass { get; set; }

    [JsonPropertyName("primarySchoolId")]
    public int PrimarySchoolId { get; set; }

    [JsonPropertyName("primarySchoolName")]
    public string PrimarySchoolName { get; set; }

    [JsonPropertyName("secondarySchoolIds")]
    public List<int> SecondarySchoolIds { get; set; } = new List<int>();

    [JsonPropertyName("secondarySchoolNames")]
    public string SecondarySchoolNames { get; set; }

    [JsonPropertyName("isHigherEducationInfoVisible")]
    public bool IsHigherEducationInfoVisible { get; set; }

    [JsonPropertyName("isStudying")]
    public bool IsStudying { get; set; }

    [JsonPropertyName("university")]
    public string University { get; set; }

    [JsonPropertyName("degree")]
    public string Degree { get; set; }

    [JsonPropertyName("isEmploymentInfoVisible")]
    public bool IsEmploymentInfoVisible { get; set; }

    [JsonPropertyName("isWorking")]
    public bool IsWorking { get; set; }

    [JsonPropertyName("organization")]
    public string Organization { get; set; }

    [JsonPropertyName("jobTitle")]
    public string JobTitle { get; set; }

    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; } = new List<string>();

    [JsonIgnore]
    public string SecurityKey { get; set; }

    // Additional properties for Events
    public DateTimeOffset? VenueCheckInDateTime { get; set; }

    public DateTimeOffset? GiftCheckInDateTime { get; set; }

    public DateTimeOffset? MealCheckInDateTime { get; set; }

    /// <summary>
    /// Validate the UserInfo object.
    /// </summary>
    /// <returns></returns>
    public bool IsValidToUpdate()
    {
        return !string.IsNullOrEmpty(Name)
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

    public ProfileEntity ConvertToUpdateEntity(HttpContext httpContext)
    {
        User contextUserData = httpContext.Items[NameConstants.USER_KEY] as User;
        ProfileEntity profileEntity = new ProfileEntity()
        {
            RowKey = Id,
            Name = Name,
            Email = contextUserData.Email,
            Gender = Gender,
            IsContactInfoVisible = IsContactInfoVisible,
            Phone = Phone,
            Location = Location,
            Latitude = Latitude,
            Longitude = Longitude,
            Photo = Photo,
            ProfileType = ProfileType,
            EntryYear = EntryYear,
            EntryClass = EntryClass,
            ExitYear = ExitYear,
            ExitClass = ExitClass,
            PrimarySchoolId = PrimarySchoolId,
            PrimarySchoolName = SchoolDataHelper.GetFullSchoolName(PrimarySchoolId),
            SecondorySchoolIds = SecondarySchoolIds.Any() ? string.Join(",", SecondarySchoolIds) : string.Empty,
            SecondarySchoolNames = SecondarySchoolIds.Any() ? SchoolDataHelper.GetFullSchoolsName(SecondarySchoolIds) : string.Empty,
            IsHigherEducationInfoVisible = IsHigherEducationInfoVisible,
            IsStudying = IsStudying,
            University = University,
            Degree = Degree,
            IsEmploymentInfoVisible = IsEmploymentInfoVisible,
            IsWorking = IsWorking,
            Organisation = Organization,
            JobTitle = JobTitle,
            Roles = string.Join(",", contextUserData.Roles),
            ModifiedBy = contextUserData.Id,
        };

        if (ProfileType == Enums.ProfileType.Student.ToString())
        {
            profileEntity.IsStudying = true;
            profileEntity.IsWorking = false;
            profileEntity.Organisation = string.Empty;
            profileEntity.JobTitle = string.Empty;
            profileEntity.ExitClass = 0;
            profileEntity.ExitYear = 0;
        }
        else if (ProfileType == Enums.ProfileType.Staff.ToString())
        {
            profileEntity.IsStudying = false;
            profileEntity.IsWorking = true;
            profileEntity.ExitClass = 0;
            profileEntity.ExitYear = 0;
            profileEntity.EntryClass = 0;
        }
        else if (ProfileType == Enums.ProfileType.ExStaff.ToString())
        {
            profileEntity.EntryClass = 0;
            profileEntity.ExitClass = 0;
        }

        return profileEntity;
    }
}
