using System.Text.Json.Serialization;
using EventManager.App.Api.Basic.Constants;

namespace EventManager.App.Api.Basic.Models;

/// <summary>
/// The <see cref="UserEntity"/> class represents an user entity.
/// </summary>
public class UserEntity : BaseEntity
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    [JsonIgnore]
    public string SecurityKey { get; set; }

    public string Roles { get; set; } = Role.User.ToString();

    /// <summary>
    /// Implicit convert to User
    /// </summary>
    /// <param name="userEntity"></param>
    public static implicit operator User(UserEntity userEntity)
    {
        if (userEntity is not null)
        {
            User user = new User()
            {
                Id = userEntity.RowKey,
                TenantId = userEntity.PartitionKey,
                Name = userEntity.Name,
                Email = userEntity.Email,
                Phone = userEntity.Phone,
                SecurityKey = userEntity.SecurityKey,
                Roles = userEntity.Roles,
                CreatedAt = userEntity.CreatedAt,
                CreatedBy = userEntity.CreatedBy
            };

            return user;
        }
        else
        {
            return null;
        }
    }
}
