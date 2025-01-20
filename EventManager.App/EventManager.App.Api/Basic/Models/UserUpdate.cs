using EventManager.App.Api.Basic.Constants;

namespace EventManager.App.Api.Basic.Models;

/// <summary>
/// The <see cref="UserUpdate"/> class represents an user update entity.
/// </summary>
public class UserUpdate
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public UserEntity ToUpdateUserEntity(HttpContext httpContext, User user)
    {
        User contextUserInfo = (User)httpContext.Items[NameConstants.USER_KEY];
        return new UserEntity
        {
            Name = Name,
            Phone = Phone,
            Email = Email,
            PartitionKey = user.TenantId,
            RowKey = user.Id,
            Roles = Role.User.ToString(),
            CreatedAt = user.CreatedAt,
            CreatedBy = user.CreatedBy,
            ModifiedBy = contextUserInfo.Id,
            SecurityKey = user.SecurityKey,
        };
    }
}
