using Azure;
using Azure.Data.Tables;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using Microsoft.Extensions.Options;

namespace EventManager.App.Api.Extended.Services;

public class MentorshipRepository : IMentorshipRepository
{
    private readonly TableClient tableClient;

    public MentorshipRepository(IOptions<AzureTableConfig> azureTableConfig)
    {
        TableServiceClient tableServiceClient = new TableServiceClient(azureTableConfig.Value.ConnectionString);
        tableClient = tableServiceClient.GetTableClient(azureTableConfig.Value.MentorshipsTable);
    }

    /// <inheritdoc/>
    public MentorshipEntity GetMentorship(string rowKey)
    {
        return tableClient.Query<MentorshipEntity>(e => e.RowKey.Equals(rowKey, StringComparison.Ordinal)).FirstOrDefault();
    }

    /// <inheritdoc/>
    public List<MentorshipEntity> GetMentorships()
    {
        return tableClient.Query<MentorshipEntity>().OrderByDescending(e => e.Timestamp).ToList();
    }

    /// <inheritdoc/>
    public bool CreateMentorship(MentorshipEntity mentorshipEntity)
    {
        Response response = tableClient.AddEntity(mentorshipEntity);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }

    /// <inheritdoc/>
    public bool UpdateMentorship(MentorshipEntity mentorshipEntity)
    {
        Response response = tableClient.UpdateEntity(mentorshipEntity, ETag.All, TableUpdateMode.Merge);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }

    /// <inheritdoc/>
    public bool DeleteMentorship(string partitionKey, string rowKey)
    {
        Response response = tableClient.DeleteEntity(partitionKey, rowKey);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }
}
