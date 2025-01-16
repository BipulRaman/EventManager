using EventManager.App.Api.Basic.Constants;

namespace EventManager.App.Api.Basic.Models;

/// <summary>
/// The <see cref="UserEntity"/> class represents an user entity.
/// </summary>
public class UserEntity : BaseEntity
{
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
    public string SecurityKey { get; set; }

    /// <summary>
    /// Gets or sets the roles assigned to the user.
    /// </summary>
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
                CreatedAt = userEntity.CreatedAt
            };

            return user;
        }
        else
        {
            return null;
        }
    }
}
