namespace EventManager.App.Api.Basic.Models;

using EventManager.App.Api.Basic.Constants;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

/// <summary>
/// The <see cref="User"/> class represents an user entity.
/// </summary>
public class User
{
    public string Id { get; set; }

    public string TenantId { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    [JsonIgnore]
    public string SecurityKey { get; set; }

    public string Roles { get; set; } = Role.User.ToString();

    public DateTimeOffset? CreatedAt { get; set; }

    public string CreatedBy { get; set; }

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email) && !new EmailAddressAttribute().IsValid(Email) && !string.IsNullOrEmpty(Phone);
    }

    public UserEntity ToUserEntity()
    {
        return new UserEntity
        {
            Name = Name,
            Phone = Phone,
            Email = Email,
            PartitionKey = TenantId,
            RowKey = Id,
            Roles = Roles,
            CreatedAt = CreatedAt,
            CreatedBy = CreatedBy,
            SecurityKey = SecurityKey,
        };
    }
}
