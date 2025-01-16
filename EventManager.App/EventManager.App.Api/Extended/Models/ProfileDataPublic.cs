using System.Text.Json.Serialization;

namespace EventManager.App.Api.Extended.Models
{
    public class ProfileDataPublic
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

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
    }
}
