using EventManager.App.Api.Basic.Models;

namespace EventManager.App.Api.Basic.Interfaces;

/// <summary>
/// The <see cref="IUserRepository{T}"/> interface represents an interface for handling user-related operations.
/// </summary>
/// <typeparam name="T">The type of user.</typeparam>
public interface IUserRepository
{
    /// <summary>
    /// Get a user from the database using the email.
    /// </summary>
    /// <param name="tenantId">The tenantId of the user to check.</param>
    /// <param name="email">The email of the user.</param>
    /// <returns>The user with the specified email.</returns>
    User GetByEmail(string email);

    /// <summary>
    /// Get a user from the database using the ID.
    /// </summary>
    /// <param name="tenantId">The tenantId of the user to check.</param>
    /// <param name="id">The ID of the user.</param>
    /// <returns>The user with the specified ID.</returns>
    User GetById(string id);

    /// <summary>
    /// Add a user to the database.
    /// </summary>
    /// <param name="user">The user to be added.</param>
    /// <returns>True if the user was successfully added, false otherwise.</returns>
    bool Create(UserEntity user);

    /// <summary>
    /// Update a user in the database.
    /// </summary>
    /// <param name="user">The user to be updated.</param>
    /// <returns>True if the user was successfully updated, false otherwise.</returns>
    bool Update(UserEntity user);

    /// <summary>
    /// Delete a user from the database.
    /// </summary>
    /// <param name="tenantId">The tenantId of the user to check.</param>
    /// <param name="id">The ID of the user to be deleted.</param>
    /// <returns>True if the user was successfully deleted, false otherwise.</returns>
    bool Delete(string tenantId, string id);

    /// <summary>
    /// Check if a user exists in the database.
    /// </summary>
    /// <param name="tenantId">The tenantId of the user to check.</param>
    /// <param name="email">The email of the user to check.</param>
    /// <returns>True if the user exists in the database, false otherwise.</returns>
    bool DoesExist(string email);
}