using EventManager.App.Api.Basic.Interfaces;
using EventManager.App.Api.Basic.Models;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace EventManager.App.Api.Basic.Services;

/// <summary>
/// The <see cref="EmailService"/> class provides the methods for email service.
/// </summary>
public class EmailService : IEmailService
{
    private readonly EmailConfig emailConfig;
    private readonly SmtpClient smtpClient;

    public EmailService(IOptions<EmailConfig> emailConfig)
    {
        this.emailConfig = emailConfig.Value;
        smtpClient = new SmtpClient(this.emailConfig.SmtpServer)
        {
            Port = this.emailConfig.Port,
            Credentials = new System.Net.NetworkCredential(this.emailConfig.UserName, this.emailConfig.Password),
            EnableSsl = true
        };

    }

    /// <inheritdoc/>
    public void SendEmail(string email, string subject, string message)
    {
        MailMessage mailMessage = new MailMessage(emailConfig.From, email, subject, message);
        mailMessage.IsBodyHtml = true;
        smtpClient.Send(mailMessage);
    }

    /// <inheritdoc/>
    public void SendOtp(string email, string otp)
    {
        string subject = "OTP for Email Verification";
        string message = $"Your OTP for Email Verification is {otp}";
        SendEmail(email, subject, message);
    }

    /// <inheritdoc/>
    public void SentInvite(string email, string senderName)
    {
        string subject = $"{senderName} invited you to the Portal";
        string message =
            $$"""
             <!DOCTYPE html>
            <html>

            <head>
                <title>Invitation Email</title>
            </head>

            <body style="font-family: Arial, sans-serif; background-color: #f2f2f2; margin: 0; padding: 0;">
                <table role="presentation" width="100%" cellspacing="0" cellpadding="0" border="0"
                    style="background-color: #ffffff; padding: 20px 0;">
                    <tr>
                        <td align="center">
                            <h1>Join the Navodaya Alumni Portal</h1>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="padding: 0 15px;">
                            <p>Welcome to the Portal.</p>
                        </td>
                    </tr>
                </table>
            </body>

            </html>
            """;
        SendEmail(email, subject, message);
    }
}
