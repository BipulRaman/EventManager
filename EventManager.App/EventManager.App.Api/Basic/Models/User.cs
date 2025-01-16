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
    /// <summary>
    /// Gets or sets the ID of the user.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user.
    /// </summary>
    public string TenantId { get; set; }

    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the user.
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Gets or sets the security key of the user.
    /// </summary>
    [JsonIgnore]
    public string SecurityKey { get; set; }

    /// <summary>
    /// Gets or sets the roles assigned to the user.
    /// </summary>
    public string Roles { get; set; } = Role.User.ToString();

    /// <summary>
    /// Gets or sets the creation timestamp of the user entity in the table storage.
    /// </summary>
    public DateTimeOffset? CreatedAt { get; set; }

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email) && !new EmailAddressAttribute().IsValid(Email) && !string.IsNullOrEmpty(Phone);
    }

    /// Implicit Convert to UserEntity
    public static implicit operator UserEntity(User user)
    {
        UserEntity userEntity = new UserEntity()
        {
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone,
            SecurityKey = user.SecurityKey,
            Roles = user.Roles,
            CreatedAt = user.CreatedAt,
            PartitionKey = user.TenantId,
            RowKey = user.Id,
            Timestamp = user.CreatedAt
        };

        return userEntity;
    }
}
