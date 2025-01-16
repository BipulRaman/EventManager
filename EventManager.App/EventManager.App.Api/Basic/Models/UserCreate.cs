using EventManager.App.Api.Basic.Constants;

namespace EventManager.App.Api.Basic.Models;

/// <summary>
/// The <see cref="UserCreate"/> class represents an user creation entity.
/// </summary>
public class UserCreate : User
{
    // Implicit Convert to UserEntity
    public static implicit operator UserEntity(UserCreate userCreate)
    {
        DateTimeOffset currentTimeOffset = DateTimeOffset.UtcNow;
        return new UserEntity
        {
            Name = userCreate.Name,
            Email = userCreate.Email,
            Phone = userCreate.Phone,
            SecurityKey = Guid.NewGuid().ToString(),
            Roles = Role.User.ToString(),
            CreatedAt = currentTimeOffset,
            Timestamp = currentTimeOffset,
            PartitionKey = NameConstants.USER_SERVICE_PARTITION_KEY,
            RowKey = Guid.NewGuid().ToString()
        };
    }
}
