namespace EventManager.App.Api.Basic.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// The <see cref="LoginInfo"/> class represents the login information.
/// </summary>
public class LoginInfo
{
    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    //[JsonPropertyName("email")]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the one-time password (OTP) for the user.
    /// </summary>
    //[JsonPropertyName("otp")]
    public string Otp { get; set; }

    /// <summary>
    /// Validates the LoginInfo object.
    /// </summary>
    /// <returns>True if the LoginInfo object is valid; otherwise, false.</returns>
    public bool IsValid()
    {
        var emailAddressAttribute = new EmailAddressAttribute();
        bool validationResult = !string.IsNullOrEmpty(Email) && emailAddressAttribute.IsValid(Email) && !string.IsNullOrEmpty(Otp);
        return validationResult;
    }
}