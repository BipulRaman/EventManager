namespace EventManager.App.Api.Basic.Services;

using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Interfaces;
using EventManager.App.Api.Basic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;

/// <summary>
/// The <see cref="AuthHandler"/> class implements the <see cref="IAuthHandler"/> interface for handling authentication operations.
/// </summary>
public class AuthHandler : IAuthHandler
{
    private readonly IUserRepository userService;
    private readonly IEmailService emailService;
    private readonly IOtpService otpService;
    private readonly ITokenService tokenService;
    private readonly ILogger<AuthHandler> logger;

    public AuthHandler(IUserRepository userService, IEmailService emailService, IOtpService otpService, ITokenService tokenService, ILogger<AuthHandler> logger)
    {
        this.userService = userService;
        this.emailService = emailService;
        this.otpService = otpService;
        this.tokenService = tokenService;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public OpResult<bool> HandleOtpRequest(OtpInfo otpInfo)
    {
        logger.LogInformation($"{nameof(AuthHandler)}.{nameof(HandleOtpRequest)} => Method started for {otpInfo.Email}.");
        OpResult<bool> result = new OpResult<bool>
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
            Result = false
        };

        try
        {
            if (otpInfo is not null && otpInfo.IsValid())
            {
                User userData = userService.GetByEmail(otpInfo.Email);
                if (userData != null && userData.Email.Equals(otpInfo.Email, StringComparison.OrdinalIgnoreCase))
                {
                    string otp = otpService.GenerateOtp(userData.SecurityKey);
                    emailService.SendOtp(otpInfo.Email, otp);
                    result.Status = HttpStatusCode.OK;
                    result.ErrorCode = 0;
                }
                else
                {
                    result.Status = HttpStatusCode.NotFound;
                    result.ErrorCode = ErrorCode.Entity_NotFound;
                }
            }
            else
            {
                result.Status = HttpStatusCode.BadRequest;
                result.ErrorCode = ErrorCode.Common_BadRequest;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(AuthHandler)}.{nameof(HandleOtpRequest)} => Error while generating OTP.");
        }

        logger.LogInformation($"{nameof(AuthHandler)}.{nameof(HandleOtpRequest)} => Method completed for {otpInfo.Email}.");
        return result;
    }

    public OpResult<string> HandleAdminOtpRequest(HttpContext httpContext, LoginInfo loginInfo)
    {
        logger.LogInformation($"{nameof(AuthHandler)}.{nameof(HandleAdminOtpRequest)} => Method started for {loginInfo.Email}.");
        OpResult<string> opResult = new OpResult<string>
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
            Result = null
        };

        try
        {
            User contextUserInfo = (User)httpContext.Items[NameConstants.USER_KEY];
            if (loginInfo is not null && loginInfo.IsValid() && otpService.ValidateOtp(contextUserInfo.SecurityKey, loginInfo.Otp))
            {
                User userData = userService.GetByEmail(loginInfo.Email);
                if (userData != null && userData.Email.Equals(loginInfo.Email, StringComparison.OrdinalIgnoreCase))
                {
                    string otp = otpService.GenerateOtp(userData.SecurityKey);
                    opResult.Result = otp;
                    opResult.Status = HttpStatusCode.OK;
                    opResult.ErrorCode = 0;
                }
                else
                {
                    opResult.Status = HttpStatusCode.NotFound;
                    opResult.ErrorCode = ErrorCode.Entity_NotFound;
                }
            }
            else
            {
                opResult.Status = HttpStatusCode.BadRequest;
                opResult.ErrorCode = ErrorCode.Common_BadRequest;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(AuthHandler)}.{nameof(HandleAdminOtpRequest)} => Error while generating OTP.");
        }

        logger.LogInformation($"{nameof(AuthHandler)}.{nameof(HandleAdminOtpRequest)} => Method completed for {loginInfo.Email}.");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<AuthTokenInfo> HandleTokenRequest(LoginInfo loginInfo)
    {
        logger.LogInformation($"{nameof(AuthHandler)}.{nameof(HandleTokenRequest)} => Method started for {loginInfo.Email}.");
        OpResult<AuthTokenInfo> result = new OpResult<AuthTokenInfo>
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
            Result = null
        };

        try
        {
            if (loginInfo is not null && loginInfo.IsValid())
            {
                User tentativeUserInfo = userService.GetByEmail(loginInfo.Email);
                if (tentativeUserInfo is not null)
                {
                    if (otpService.ValidateOtp(tentativeUserInfo.SecurityKey, loginInfo.Otp))
                    {
                        if (!string.IsNullOrWhiteSpace(tentativeUserInfo.Roles) && tentativeUserInfo.Roles.Split(",").Length > 0)
                        {
                            AuthTokenInfo authTokenInfo = tokenService.GenerateToken(tentativeUserInfo);
                            result.Result = authTokenInfo;
                            result.Status = HttpStatusCode.OK;
                            result.ErrorCode = 0;
                        }
                        else
                        {
                            result.Status = HttpStatusCode.Forbidden;
                            result.ErrorCode = ErrorCode.Common_Forbidden;
                        }
                    }
                    else
                    {
                        result.Status = HttpStatusCode.BadRequest;
                        result.ErrorCode = ErrorCode.OtpService_OtpVerificationFailed;
                    }
                }
                else
                {
                    result.Status = HttpStatusCode.NotFound;
                    result.ErrorCode = ErrorCode.Entity_NotFound;
                }
            }
            else
            {
                result.Status = HttpStatusCode.BadRequest;
                result.ErrorCode = ErrorCode.Common_BadRequest;
            }

        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(AuthHandler)}.{nameof(HandleTokenRequest)} => Error while generating token.");
        }

        logger.LogInformation($"{nameof(AuthHandler)}.{nameof(HandleTokenRequest)} => Method completed for {loginInfo.Email}.");
        return result;
    }

    /// <inheritdoc/>
    public OpResult<bool> HandleAuthReset(HttpContext httpContext)
    {
        logger.LogInformation($"{nameof(AuthHandler)}.{nameof(HandleAuthReset)} => Method started.");
        OpResult<bool> opResult = new OpResult<bool>
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
            Result = false
        };

        try
        {
            User contextUserInfo = (User)httpContext.Items[NameConstants.USER_KEY];
            if (contextUserInfo is not null)
            {
                User user = userService.GetByEmail(contextUserInfo.Email);
                UserEntity userEntity = user.ToUserEntity();
                userEntity.SecurityKey = Guid.NewGuid().ToString();
                userEntity.ModifiedBy = contextUserInfo.Id;

                if (user is not null)
                {
                    user.SecurityKey = Guid.NewGuid().ToString();
                    bool isUserUpdated = userService.Update(userEntity);

                    if (isUserUpdated)
                    {
                        opResult.ErrorCode = ErrorCode.None;
                        opResult.Status = HttpStatusCode.OK;
                        opResult.Result = true;
                    }
                    else
                    {
                        opResult.ErrorCode = ErrorCode.Entity_Update_Failed;
                        opResult.Status = HttpStatusCode.InternalServerError;
                    }
                }
                else
                {
                    opResult.Status = HttpStatusCode.NotFound;
                    opResult.ErrorCode = ErrorCode.Entity_NotFound;
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(AuthHandler)}.{nameof(HandleAuthReset)} => Error while resetting auth.");
        }
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<string> HandleOtpSecretRequest(HttpContext httpContext)
    {
        logger.LogInformation($"{nameof(AuthHandler)}.{nameof(HandleOtpSecretRequest)} => Method started.");

        OpResult<string> opResult = new OpResult<string>
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
            Result = null
        };

        try
        {
            User contextUserInfo = (User)httpContext.Items[NameConstants.USER_KEY];
            if (contextUserInfo is not null)
            {
                string otpSecret = otpService.GenerateAuthKey(contextUserInfo.SecurityKey);
                if (!string.IsNullOrEmpty(otpSecret))
                {
                    opResult.ErrorCode = ErrorCode.None;
                    opResult.Status = HttpStatusCode.OK;
                    opResult.Result = otpSecret;
                }
                else
                {
                    opResult.ErrorCode = ErrorCode.Common_InternalServerError;
                    opResult.Status = HttpStatusCode.InternalServerError;
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(AuthHandler)}.{nameof(HandleOtpSecretRequest)} => Error while requesting Auth Secret.");
        }
        return opResult;
    }

    public OpResult<bool> HandleVerifyEmailRequest(string email)
    {
        logger.LogInformation($"{nameof(AuthHandler)}.{nameof(HandleVerifyEmailRequest)} => Method started for {email}.");
        OpResult<bool> opResult = new OpResult<bool>
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
            Result = false
        };

        try
        {
            // Validate email
            if (string.IsNullOrEmpty(email) || !new EmailAddressAttribute().IsValid(email))
            {
                opResult.Status = HttpStatusCode.BadRequest;
                opResult.ErrorCode = ErrorCode.Common_BadRequest;
                return opResult;
            }
            else
            {
                int sixDigitPassword = otpService.GenerateStaticPassword(email);
                emailService.SendOtp(email, sixDigitPassword.ToString());
                opResult.Status = HttpStatusCode.OK;
                opResult.ErrorCode = 0;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(AuthHandler)}.{nameof(HandleVerifyEmailRequest)} => Error while verifying email.");
        }
        return opResult;
    }
}
