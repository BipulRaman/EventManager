namespace EventManager.App.Api.Basic.Constants;

/// <summary>
/// The <see cref="Role"/> enum represents the roles that a user can have.
/// </summary>
public enum Role
{
    /// <summary>
    /// Represents a super admin role.
    /// </summary>
    SuperAdmin,

    /// <summary>
    /// Represents an admin role.
    /// </summary>
    Admin,

    /// <summary>
    /// Represents a user role.
    /// </summary>
    User,

    /// <summary>
    /// Represents an invited user role.
    /// </summary>
    InvitedUser,

    /// <summary>
    /// Represents a creator role.
    /// </summary>
    Creator,
}
