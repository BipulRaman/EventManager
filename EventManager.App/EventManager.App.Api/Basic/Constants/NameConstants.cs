namespace EventManager.App.Api.Basic.Constants;

/// <summary>
/// The <see cref="NameConstants"/> class contains the names of various constants used throughout the application.
/// </summary>
public static class NameConstants
{
    /// <summary>
    /// The key used to identify a user.
    /// </summary>
    public const string USER_KEY = "User";

    /// <summary>
    /// The partition key used for the active user service.
    /// </summary>
    public const string USER_SERVICE_PARTITION_KEY = "ActiveUser";
}
