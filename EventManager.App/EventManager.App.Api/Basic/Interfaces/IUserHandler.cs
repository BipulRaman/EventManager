using EventManager.App.Api.Basic.Models;

namespace EventManager.App.Api.Basic.Interfaces;

/// <summary>
/// The <see cref="IUserHandler"/> interface represents an interface for handling user-related operations.
/// </summary>
public interface IUserHandler
{
    /// <summary>
    /// Handles the get user request.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    OpResult<User> HandleGetUser(string id);

    /// <summary>
    /// Handles the create user request.
    /// </summary>
    /// <param name="userCreate"></param>
    /// <returns></returns>
    OpResult<bool> HandleCreateUser(UserCreate userCreate);

    /// <summary>
    /// Handles the update user request.
    /// </summary>
    /// <param name="userUpdate"></param>
    /// <returns></returns>
    OpResult<bool> HandleUpdateUser(HttpContext httpContext, UserUpdate userUpdate);

    /// <summary>
    /// Handles the delete user request.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    OpResult<bool> HandleDeleteUser(string id);
}
