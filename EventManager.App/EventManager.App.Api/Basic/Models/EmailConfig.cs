namespace EventManager.App.Api.Basic.Models;

/// <summary>
/// The <see cref="EmailConfig"/> class represents the configuration for email.
/// </summary>
public class EmailConfig
{
    /// <summary>
    /// Gets or sets the SMTP server.
    /// </summary>
    public string SmtpServer { get; set; }

    /// <summary>
    /// Gets or sets the port of the SMTP server.
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Gets or sets the username for the SMTP server.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Gets or sets the password for the SMTP server.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the email address from which the email will be sent.
    /// </summary>
    public string From { get; set; }

    /// <summary>
    /// Gets or sets the Azure Communication Service connection string.
    /// </summary>
    public string AzCommConnectionString { get; set; }
}
