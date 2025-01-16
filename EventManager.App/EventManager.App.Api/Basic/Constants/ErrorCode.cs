namespace EventManager.App.Api.Basic.Constants;

/// <summary>
/// The <see cref="ErrorCode"/> enum represents the error codes that can be returned by the application.
/// </summary>
public enum ErrorCode
{
    None = 0,

    // Common Error Codes    
    Common_InternalServerError = 1001,
    Common_BadRequest = 1002,
    Common_Unauthorized = 1003,
    Common_Forbidden = 1004,
    Common_UserLocked = 1005,

    // User Service Error Codes
    Entity_Create_Failed = 2001,
    Entity_Update_Failed = 2002,
    Entity_Delete_Failed = 2003,
    Entity_Read_Failed = 2004,
    Entity_Collection_Read_Failed = 2005,
    Entity_NotFound = 2006,
    Entity_AlreadyExist = 2007,
    Entity_Mismatched = 2008,
    Entity_Unauthorized = 2009,

    // Email Service Error Codes
    EmailService_EmailSendFailed = 3001,

    // OTP Service Error Codes
    OtpService_OtpGenerationFailed = 4001,
    OtpService_OtpVerificationFailed = 4002,

    // Token Service Error Codes
    TokenService_TokenGenerationFailed = 5001,
    TokenService_TokenValidationFailed = 5002,
}