using Microsoft.Extensions.Options;
using Azure.Communication.Email;
using Azure;
using EventManager.App.Api.Basic.Interfaces;
using EventManager.App.Api.Basic.Models;

namespace EventManager.App.Api.Basic.Services;

/// <summary>
/// The <see cref="EmailService"/> class provides the methods for email service.
/// </summary>
public class AzEmailService : IEmailService
{
    private readonly EmailConfig emailConfig;
    private readonly EmailClient emailClient;
    private readonly ILogger<AzEmailService> logger;

    public AzEmailService(IOptions<EmailConfig> emailConfig, ILogger<AzEmailService> logger)
    {
        this.emailConfig = emailConfig.Value;
        emailClient = new EmailClient(this.emailConfig.AzCommConnectionString);
        this.logger = logger;
    }

    /// <inheritdoc/>
    public void SendEmail(string email, string subject, string message)
    {
        logger.LogInformation($"{nameof(AzEmailService)}.{nameof(SendEmail)} => Method started for {email} with subject {subject}.");
        var emailMessage = new EmailMessage(
            senderAddress: emailConfig.From,
            content: new EmailContent(subject)
            {
                PlainText = message,
                Html = message
            },
            recipients: new EmailRecipients(new List<EmailAddress> { new EmailAddress(email) }));
        EmailSendOperation emailSendOperation = emailClient.Send(WaitUntil.Completed, emailMessage);
        logger.LogInformation($"{nameof(AzEmailService)}.{nameof(SendEmail)} => Method executed for {email} with CorrelationId {emailSendOperation.Id}");
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
