namespace EventManager.App.Api.Basic.Models;

/// <summary>
/// The <see cref="AzureTableConfig"/> class represents the configuration for Azure Table Storage.
/// </summary>
public class AzureTableConfig
{
    /// <summary>
    /// Gets or sets the connection string for Azure Table Storage.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Gets or sets the table name for user in Azure Table Storage.
    /// </summary>
    public string UserTable { get; set; }

    /// <summary>
    /// Gets or sets the blob container name for Profile Photos.
    /// </summary>
    public string ProfilePhotosContainer { get; set; }

    /// <summary>
    /// Gets or sets the table name for Notifications in Azure Table Storage.
    /// </summary>
    public string NotificationsTable { get; set; }

    /// <summary>
    /// Gets or sets the table name for Mentorship in Azure Table Storage.
    /// </summary>
    public string MentorshipsTable { get; set; }

    /// <summary>
    /// Gets or sets the table name for Events in Azure Table Storage.
    /// </summary>
    public string EventsTable { get; set; }

    /// <summary>
    /// Gets or sets the table name for Expenses in Azure Table Storage.
    /// </summary>
    public string ExpensesTable { get; set; }

    /// <summary>
    /// Gets or sets the table name for Business in Azure Table Storage.
    /// </summary>
    public string BusinessesTable { get; set; }

    /// <summary>
    /// Gets or sets the table name for Posts in Azure Table Storage.
    /// </summary>
    public string PostsTable { get; set; }

    /// <summary>
    /// Gets or sets the maz result size.
    /// </summary>
    public int MaxResultPageSize { get; set; }
}
