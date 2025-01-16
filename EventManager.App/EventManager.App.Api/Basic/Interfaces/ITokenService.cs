namespace EventManager.App.Api.Basic.Interfaces;

using EventManager.App.Api.Basic.Models;

/// <summary>
/// The <see cref="ITokenService"/> interface represents an interface for handling token-related operations.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generate a token for the user.
    /// </summary>
    /// <param name="user"></param>
    /// <returns>Returns the authentication token information.</returns>
    AuthTokenInfo GenerateToken(User user);

    /// <summary>
    /// Validate the token.
    /// </summary>
    /// <param name="token"></param>
    /// <returns>Returns true if the token is valid, false otherwise.</returns>
    bool ValidateToken(string token);
}
