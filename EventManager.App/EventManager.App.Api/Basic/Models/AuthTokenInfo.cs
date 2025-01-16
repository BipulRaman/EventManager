namespace EventManager.App.Api.Basic.Models;

using System.Collections.Generic;

/// <summary>
/// The <see cref="AuthTokenInfo"/> class represents the authentication token information.
/// </summary>
public class AuthTokenInfo
{
    /// <summary>
    /// Gets or sets the token type.
    /// </summary>
    //[JsonPropertyName("token_type")]
    public string TokenType { get; set; } = "Bearer";

    /// <summary>
    /// Gets or sets the expiration time in seconds.
    /// </summary>
    //[JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    /// <summary>
    /// Gets or sets the extended expiration time in seconds.
    /// </summary>
    //[JsonPropertyName("ext_expires_in")]
    public int ExtExpiresIn { get; set; }

    /// <summary>
    /// Gets or sets the expiration time in milliseconds since Unix epoch.
    /// </summary>
    //[JsonPropertyName("expires_on")]
    public int ExpiresOn { get; set; }

    /// <summary>
    /// Gets or sets the time before which the token is not valid in milliseconds since Unix epoch.
    /// </summary>
    //[JsonPropertyName("not_before")]
    public int NotBefore { get; set; }

    /// <summary>
    /// Gets or sets the resource.
    /// </summary>
    //[JsonPropertyName("resource")]
    public string Resource { get; set; }

    /// <summary>
    /// Gets or sets the access token.
    /// </summary>
    //[JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    //[JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    //[JsonPropertyName("user_id")]
    public string UserId { get; set; }

    /// <summary>
    /// Gets or sets the list of roles.
    /// </summary>
    //[JsonPropertyName("roles")]
    public List<string> Roles { get; set; }
}
