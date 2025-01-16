namespace EventManager.App.Api.Basic.Interfaces;

/// <summary>
/// The <see cref="IEmailService"/> interface provides the methods for email service.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Send email to the user.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="subject"></param>
    /// <param name="message"></param>
    void SendEmail(string email, string subject, string message);

    /// <summary>
    /// Send OTP to the user.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="otp"></param>
    void SendOtp(string email, string otp);

    /// <summary>
    /// Send email invite.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="senderName"></param>
    void SentInvite(string email, string senderName);
}
