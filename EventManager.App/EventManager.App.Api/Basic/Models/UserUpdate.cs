using EventManager.App.Api.Basic.Constants;

namespace EventManager.App.Api.Basic.Models;

/// <summary>
/// The <see cref="UserUpdate"/> class represents an user update entity.
/// </summary>
public class UserUpdate : User
{
    /// <summary>
    /// Method to convert UserUpdate to UserEntity
    /// </summary>
    /// <param name="userUpdate"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public UserEntity ToUserEntity(HttpContext httpContext)
    {
        User contextUserInfo = (User)httpContext.Items[NameConstants.USER_KEY];
        return new UserEntity
        {
            Name = Name,
            Phone = Phone,
            Email = Email,
            PartitionKey = contextUserInfo.TenantId,
            RowKey = contextUserInfo.Id,
            Roles = contextUserInfo.Roles,
            CreatedAt = contextUserInfo.CreatedAt,
            SecurityKey = contextUserInfo.SecurityKey,
        };
    }
}
