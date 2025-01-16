namespace EventManager.App.Api.Basic.Interfaces;

using EventManager.App.Api.Basic.Models;
using Microsoft.AspNetCore.Http;

/// <summary>
/// The <see cref="IAuthHandler"/> interface represents an interface for handling authentication-related operations.
/// </summary>
public interface IAuthHandler
{
    /// <summary>
    /// Handles the OTP request.
    /// </summary>
    /// <param name="otpInfo">The OTP information.</param>
    /// <returns>The operation result indicating whether the OTP request was handled successfully.</returns>
    OpResult<bool> HandleOtpRequest(OtpInfo otpInfo);

    /// <summary>
    /// Handles the Admin OTP request.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <param name="otpInfo">The OTP information.</param>
    /// <returns>The operation result indicating whether the OTP request was handled successfully.</returns>
    OpResult<string> HandleAdminOtpRequest(HttpContext httpContext, LoginInfo loginInfo);

    /// <summary>
    /// Handles the token request.
    /// /// </summary>
    /// <param name="loginInfo">The login information.</param>
    /// <returns>The operation result containing the authentication token information.</returns>
    OpResult<AuthTokenInfo> HandleTokenRequest(LoginInfo loginInfo);

    /// <summary>
    /// Handles the authentication reset request.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <returns>The operation result indicating whether the authentication reset was handled successfully.</returns>
    OpResult<bool> HandleAuthReset(HttpContext httpContext);

    /// <summary>
    /// Handles the OTP secret request.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <returns>The operation result containing the OTP secret.</returns>
    OpResult<string> HandleOtpSecretRequest(HttpContext httpContext);

    /// <summary>
    /// Handles the verify email request.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    OpResult<bool> HandleVerifyEmailRequest(string email);
}
