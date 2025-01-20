using EventManager.App.Api.Basic.Constants;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace EventManager.App.Api.Basic.Models;

/// <summary>
/// The <see cref="UserCreate"/> class represents an user creation entity.
/// </summary>
public class UserCreate
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    
    public UserEntity ToUserEntity(HttpContext httpContext)
    {
        User contextUserInfo = (User)httpContext.Items[NameConstants.USER_KEY];
        return new UserEntity
        {
            Name = Name,
            Phone = Phone,
            Email = Email,
            PartitionKey = contextUserInfo.TenantId,
            RowKey = Guid.NewGuid().ToString(),
            Roles = Role.User.ToString(),
            CreatedAt = DateTimeOffset.UtcNow,
            CreatedBy = contextUserInfo.Id,
            ModifiedBy = contextUserInfo.Id,
            SecurityKey = Guid.NewGuid().ToString(),
        };
    }
}
