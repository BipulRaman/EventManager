namespace EventManager.App.Api.Basic.Models;

/// <summary>
/// <see cref="JwtConfig"/> class represents the configuration for JWT.
/// </summary>
public class JwtConfig
{
    /// <summary>
    /// Gets or sets the secret key used to sign the JWT.
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Gets or sets the issuer of the JWT.
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// Gets or sets the audience of the JWT.
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// Gets or sets the base key used for generating OTP (One-Time Password).
    /// </summary>
    public string OtpBaseKey { get; set; }
}