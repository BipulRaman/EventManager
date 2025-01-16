using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;
using System.Text.Json.Serialization;

namespace EventManager.App.Api.Extended.Models;

public class BusinessData : BaseData
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("details")]
    public string Details { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("pinCode")]
    public int PinCode { get; set; }

    [JsonPropertyName("mapLink")]
    public string MapLink { get; set; }

    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("offer")]
    public string Offer { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("website")]
    public string Website { get; set; }

    public bool IsValidToCreate()
    {
        return !string.IsNullOrWhiteSpace(Name)
            && !string.IsNullOrWhiteSpace(Details)
            && !string.IsNullOrWhiteSpace(Address)
            && !string.IsNullOrWhiteSpace(Phone);
    }

    public bool IsValidToUpdate()
    {
        return !string.IsNullOrWhiteSpace(Id)
            && !string.IsNullOrWhiteSpace(Name)
            && !string.IsNullOrWhiteSpace(Details)
            && !string.IsNullOrWhiteSpace(Address)
            && !string.IsNullOrWhiteSpace(Phone);
    }

    public BusinessEntity ConvertToCreateEntity(HttpContext httpContext)
    {
        User contextUserData = httpContext.Items[NameConstants.USER_KEY] as User;
        DateTime dateTime = DateTime.UtcNow;
        return new BusinessEntity
        {
            PartitionKey = contextUserData.Id,
            RowKey = Guid.NewGuid().ToString(),
            Name = Name,
            Details = Details,
            Category = Category,
            Address = Address,
            PinCode = PinCode,
            MapLink = MapLink,
            Latitude = Latitude,
            Longitude = Longitude,
            Offer = Offer,
            Phone = Phone,
            Email = Email,
            Website = Website,
            CreatedAt = new DateTimeOffset(dateTime),
            Timestamp = new DateTimeOffset(dateTime),
            CreatedBy = contextUserData.Id,
            CreatedByName = contextUserData.Name,
            ModifiedBy = contextUserData.Id
        };
    }

    public BusinessEntity ConvertToUpdateEntity(HttpContext httpContext)
    {
        User contextUserData = httpContext.Items[NameConstants.USER_KEY] as User;
        return new BusinessEntity
        {
            PartitionKey = contextUserData.Id,
            RowKey = Id,
            Name = Name,
            Details = Details,
            Category = Category,
            Address = Address,
            PinCode = PinCode,
            MapLink = MapLink,
            Latitude = Latitude,
            Longitude = Longitude,
            Offer = Offer,
            Phone = Phone,
            Email = Email,
            Website = Website,
            ModifiedBy = contextUserData.Id
        };
    }
}
