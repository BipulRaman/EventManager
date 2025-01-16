namespace EventManager.App.Api.Basic.Services;

using EventManager.App.Api.Basic.Interfaces;
using EventManager.App.Api.Basic.Models;
using Microsoft.Extensions.Options;
using OtpNet;
using System;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// The <see cref="OtpService"/> implements the <see cref="IOtpService"/> interface for handling one-time password (OTP) operations.
/// </summary>
public class OtpService : IOtpService
{
    private readonly JwtConfig jwtConfig;

    public OtpService(IOptions<JwtConfig> jwtConfig)
    {
        this.jwtConfig = jwtConfig.Value;
    }

    /// <inheritdoc/>
    public string GenerateOtp(string userSecurityKey)
    {
        byte[] bytes = GetUserAuthKeyBytesArrayUnique(userSecurityKey);
        var totp = new Totp(bytes);
        return totp.ComputeTotp();
    }

    /// <inheritdoc/>
    public bool ValidateOtp(string userSecurityKey, string otp)
    {
        string otpSecret = GenerateAuthKey(userSecurityKey);
        byte[] bytes = Base32Encoding.ToBytes(otpSecret);
        var totp = new Totp(bytes);

        VerificationWindow verificationWindow = new VerificationWindow(2, 2);
        bool validationResult = totp.VerifyTotp(otp, out long timeStepMatched, verificationWindow);
        return validationResult;
    }

    /// <inheritdoc/
    public string GenerateAuthKey(string userSecurityKey)
    {
        byte[] bytes = GetUserAuthKeyBytesArrayUnique(userSecurityKey);
        var randomKey = Base32Encoding.ToString(bytes);
        return randomKey;
    }

    /// <inheritdoc/>
    public int GenerateStaticPassword(string inputString)
    {
        string input = $"{inputString}NVSAlumni";
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            int rawPassword = BitConverter.ToInt32(hashBytes, 0);

            int sixDigitPassword = 100000 + Math.Abs(rawPassword) % 900000;
            return sixDigitPassword;
        }
    }

    private byte[] GetUserAuthKeyBytesArrayUnique(string userSecurityKey)
    {
        string base64UserSecurityKey = Convert.ToBase64String(Encoding.ASCII.GetBytes(userSecurityKey));
        string base64ServerKey = Convert.ToBase64String(Encoding.ASCII.GetBytes(jwtConfig.OtpBaseKey));
        string rawKey = base64UserSecurityKey.Insert(10, base64ServerKey);
        byte[] bytes = Encoding.ASCII.GetBytes(rawKey);
        return bytes;
    }
}
