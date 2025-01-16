using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;

namespace EventManager.App.Api.Basic.Utilities;

public static class ContextHelper
{
    public static User GetLoggedInUser(HttpContext httpContext)
    {
        return httpContext?.Items[NameConstants.USER_KEY] as User;
    }
}
