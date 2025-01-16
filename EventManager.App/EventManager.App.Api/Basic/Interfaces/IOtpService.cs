namespace EventManager.App.Api.Basic.Interfaces;

/// <summary>
/// The <see cref="IOtpService"/> interface represents an interface for handling OTP-related operations.
/// </summary>
public interface IOtpService
{
    /// <summary>
    /// Generate Security Key for the user.
    /// </summary>
    /// <param name="userSecurityKey"></param>
    /// <returns>Returns the generated Security Key.</returns>
    string GenerateAuthKey(string userSecurityKey);

    /// <summary>
    /// Generate OTP for the user.
    /// </summary>
    /// <param name="userSecurityKey"></param>
    /// <returns>Returns the generated OTP.</returns>
    string GenerateOtp(string userSecurityKey);

    /// <summary>
    /// Validate OTP for the user.
    /// </summary>
    /// <param name="userSecurityKey"></param>
    /// <param name="otp"></param>
    /// <returns>Returns true if the OTP is valid, false otherwise.</returns>
    bool ValidateOtp(string userSecurityKey, string otp);

    /// <summary>
    /// Generate Static Password for the user.
    /// </summary>
    /// <param name="inputString"></param>
    /// <returns></returns>
    int GenerateStaticPassword(string inputString);
}
