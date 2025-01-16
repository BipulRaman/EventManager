namespace EventManager.App.Api.Basic.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// The <see cref="OtpInfo"/> class represents the one-time password (OTP) information.
/// </summary>
public class OtpInfo
{
    /// <summary>
    /// Gets or sets the email address associated with the OTP.
    /// </summary>
    //[JsonPropertyName("email")]
    public string Email { get; set; }

    /// <summary>
    /// Validates the OTP information.
    /// </summary>
    /// <returns>True if the OTP information is valid; otherwise, false.</returns>
    public bool IsValid()
    {
        var emailAddressAttribute = new EmailAddressAttribute();
        bool validationResult = !string.IsNullOrEmpty(Email) && emailAddressAttribute.IsValid(Email);
        return validationResult;
    }
}
