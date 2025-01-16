using EventManager.App.Api.Basic.Models;

namespace EventManager.App.Api.Extended.Models;

public class BusinessEntity : BaseEntity
{
    public string Name { get; set; }

    public string Details { get; set; }

    public string Category { get; set; }

    public string Address { get; set; }

    public int PinCode { get; set; }

    public string MapLink { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string Offer { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string Website { get; set; }

    public static implicit operator BusinessData(BusinessEntity businessEntity)
    {
        return new BusinessData
        {
            Id = businessEntity.RowKey,
            Name = businessEntity.Name,
            Details = businessEntity.Details,
            Category = businessEntity.Category,
            Address = businessEntity.Address,
            PinCode = businessEntity.PinCode,
            MapLink = businessEntity.MapLink,
            Latitude = businessEntity.Latitude,
            Longitude = businessEntity.Longitude,
            Offer = businessEntity.Offer,
            Phone = businessEntity.Phone,
            Email = businessEntity.Email,
            Website = businessEntity.Website,
            CreatedAt = businessEntity.CreatedAt,
            ModifiedAt = businessEntity.Timestamp,
            CreatedBy = businessEntity.CreatedBy,
            CreatedByName = businessEntity.CreatedByName,
            ModifiedBy = businessEntity.ModifiedBy,
        };

    }
}
