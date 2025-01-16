namespace EventManager.App.Api.Basic.Controllers;

using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Interfaces;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Basic.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthHandler authHandler;
    private readonly ILogger<AuthController> logger;

    public AuthController(IAuthHandler authHandler, ILogger<AuthController> logger)
    {
        this.authHandler = authHandler;
        this.logger = logger;
    }

    /// <summary>
    /// Request OTP.
    /// </summary>
    /// <param name="otpInfo"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("otp")]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult OtpRequest(OtpInfo otpInfo)
    {
        logger.LogInformation($"{nameof(AuthController)}.{nameof(OtpRequest)} => Started.");
        OpResult<bool> opResult = authHandler.HandleOtpRequest(otpInfo);
        logger.LogInformation($"{nameof(AuthController)}.{nameof(OtpRequest)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    /// <summary>
    /// Request OTP.
    /// </summary>
    /// <param name="loginInfo"></param>
    /// <returns></returns>
    [Authorize(Roles = nameof(Role.SuperAdmin))]
    [HttpPost("admin/otp")]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult AdminOtpRequest(LoginInfo loginInfo)
    {
        logger.LogInformation($"{nameof(AuthController)}.{nameof(AdminOtpRequest)} => Started.");
        OpResult<string> opResult = authHandler.HandleAdminOtpRequest(HttpContext, loginInfo);
        logger.LogInformation($"{nameof(AuthController)}.{nameof(AdminOtpRequest)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    /// <summary>
    /// Request JWT Token after successful login.
    /// </summary>
    /// <param name="loginInfo"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("token")]
    [ProducesResponseType(typeof(OpResult<AuthTokenInfo>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<AuthTokenInfo>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<AuthTokenInfo>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<AuthTokenInfo>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(OpResult<AuthTokenInfo>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult TokenRequest(LoginInfo loginInfo)
    {
        logger.LogInformation($"{nameof(AuthController)}.{nameof(TokenRequest)} => Started.");

        OpResult<AuthTokenInfo> opResult = authHandler.HandleTokenRequest(loginInfo);
        logger.LogInformation($"{nameof(AuthController)}.{nameof(TokenRequest)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpGet("reset")]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult AuthReset()
    {
        logger.LogInformation($"{nameof(AuthController)}.{nameof(AuthReset)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<bool> opResult = authHandler.HandleAuthReset(HttpContext);
        logger.LogInformation($"{nameof(AuthController)}.{nameof(AuthReset)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpGet("otpsecret")]
    [ProducesResponseType(typeof(OpResult<string>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<string>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<string>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult AuthKeyRequest()
    {
        logger.LogInformation($"{nameof(AuthController)}.{nameof(AuthKeyRequest)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<string> opResult = authHandler.HandleOtpSecretRequest(HttpContext);
        logger.LogInformation($"{nameof(AuthController)}.{nameof(AuthKeyRequest)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [AllowAnonymous]
    [HttpGet("verifyemail/{email}")]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.BadRequest)]
    public IActionResult VerifyEmail(string email)
    {
        logger.LogInformation($"{nameof(AuthController)}.{nameof(VerifyEmail)} => Started.");
        OpResult<bool> opResult = authHandler.HandleVerifyEmailRequest(email);
        logger.LogInformation($"{nameof(AuthController)}.{nameof(VerifyEmail)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }
}