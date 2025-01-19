using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Extended.Utilities;

namespace EventManager.App.Api.Extended.Models;

public class ProfileEntity : BaseEntity
{
    public string Name { get; set; }

    public string Gender { get; set; }

    public bool IsContactInfoVisible { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Location { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string Photo { get; set; }

    public string ProfileType { get; set; }

    public int EntryYear { get; set; }

    public int EntryClass { get; set; }

    public int ExitYear { get; set; }

    public int ExitClass { get; set; }

    public int PrimarySchoolId { get; set; }

    public string SecondorySchoolIds { get; set; }

    public string PrimarySchoolName { get; set; }

    public string SecondarySchoolNames { get; set; }

    public bool IsHigherEducationInfoVisible { get; set; }

    public bool IsStudying { get; set; }

    public string University { get; set; }

    public string Degree { get; set; }

    public bool IsEmploymentInfoVisible { get; set; }

    public bool IsWorking { get; set; }

    public string Organisation { get; set; }

    public string JobTitle { get; set; }

    public string Roles { get; set; }

    public string SecurityKey { get; set; }

    public DateTimeOffset? VenueCheckInDateTime { get; set; }

    public DateTimeOffset? GiftCheckInDateTime { get; set; }

    public DateTimeOffset? MealCheckInDateTime { get; set; }

    public static implicit operator ProfileData(ProfileEntity userEntity)
    {
        if (userEntity is not null)
        {
            return new ProfileData()
            {
                Id = userEntity.RowKey,
                Name = userEntity.Name,
                Gender = userEntity.Gender,
                IsContactInfoVisible = userEntity.IsContactInfoVisible,
                Email = userEntity.Email,
                Phone = userEntity.Phone,
                Location = userEntity.Location,
                Latitude = userEntity.Latitude,
                Longitude = userEntity.Longitude,
                Photo = userEntity.Photo,
                ProfileType = userEntity.ProfileType,
                EntryYear = userEntity.EntryYear,
                EntryClass = userEntity.EntryClass,
                ExitYear = userEntity.ExitYear,
                ExitClass = userEntity.ExitClass,
                PrimarySchoolId = userEntity.PrimarySchoolId,
                PrimarySchoolName = SchoolDataHelper.GetFullSchoolName(userEntity.PrimarySchoolId),
                SecondarySchoolIds = SchoolDataHelper.SchoolIdsStringToList(userEntity.SecondorySchoolIds),
                SecondarySchoolNames = SchoolDataHelper.GetFullSchoolsName(userEntity.SecondorySchoolIds),
                IsHigherEducationInfoVisible = userEntity.IsHigherEducationInfoVisible,
                IsStudying = userEntity.IsStudying,
                University = userEntity.University,
                Degree = userEntity.Degree,
                IsEmploymentInfoVisible = userEntity.IsEmploymentInfoVisible,
                IsWorking = userEntity.IsWorking,
                Organization = userEntity.Organisation,
                JobTitle = userEntity.JobTitle,
                Roles = userEntity.Roles?.Split(",").ToList() ?? new List<string>(),
                SecurityKey = userEntity.SecurityKey,
                CreatedAt = userEntity.CreatedAt,
                ModifiedAt = userEntity.Timestamp,
                CreatedBy = userEntity.CreatedBy,
                CreatedByName = userEntity.CreatedByName,
                ModifiedBy = userEntity.ModifiedBy,
                VenueCheckInDateTime = userEntity.VenueCheckInDateTime,
                GiftCheckInDateTime = userEntity.GiftCheckInDateTime,
                MealCheckInDateTime = userEntity.MealCheckInDateTime,
            };
        }
        else
        {
            return null;
        }
    }

    public static implicit operator ProfileDataPublic(ProfileEntity userEntity)
    {
        return new ProfileDataPublic()
        {
            Id = userEntity.RowKey,
            Name = userEntity.Name,
            Gender = userEntity.Gender,
            IsContactInfoVisible = userEntity.IsContactInfoVisible,
            Email = userEntity.IsContactInfoVisible ? userEntity.Email : string.Empty,
            Phone = userEntity.IsContactInfoVisible ? userEntity.Phone : string.Empty,
            Location = userEntity.IsContactInfoVisible ? userEntity.Location : string.Empty,
            Photo = userEntity.Photo,
            ProfileType = userEntity.ProfileType,
            EntryYear = userEntity.EntryYear,
            EntryClass = userEntity.EntryClass,
            ExitYear = userEntity.ExitYear,
            ExitClass = userEntity.ExitClass,
            PrimarySchoolId = userEntity.PrimarySchoolId,
            PrimarySchoolName = SchoolDataHelper.GetFullSchoolName(userEntity.PrimarySchoolId),
            SecondarySchoolIds = SchoolDataHelper.SchoolIdsStringToList(userEntity.SecondorySchoolIds),
            SecondarySchoolNames = SchoolDataHelper.GetFullSchoolsName(userEntity.SecondorySchoolIds),
            IsHigherEducationInfoVisible = userEntity.IsHigherEducationInfoVisible,
            IsStudying = userEntity.IsHigherEducationInfoVisible && userEntity.IsStudying,
            University = userEntity.IsHigherEducationInfoVisible ? userEntity.University : string.Empty,
            Degree = userEntity.IsHigherEducationInfoVisible ? userEntity.Degree : string.Empty,
            IsEmploymentInfoVisible = userEntity.IsEmploymentInfoVisible,
            IsWorking = userEntity.IsEmploymentInfoVisible && userEntity.IsWorking,
            Organization = userEntity.IsEmploymentInfoVisible ? userEntity.Organisation : string.Empty,
            JobTitle = userEntity.IsEmploymentInfoVisible ? userEntity.JobTitle : string.Empty,
        };
    }
}
