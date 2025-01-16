using Azure;
using Azure.Data.Tables;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using Microsoft.Extensions.Options;

namespace EventManager.App.Api.Extended.Services;

public class PostRepository : IPostRepository
{
    private readonly TableClient tableClient;

    private readonly int maxResultPageSize;

    public PostRepository(IOptions<AzureTableConfig> azureTableConfig)
    {
        TableServiceClient tableServiceClient = new TableServiceClient(azureTableConfig.Value.ConnectionString);
        tableClient = tableServiceClient.GetTableClient(azureTableConfig.Value.PostsTable);
        maxResultPageSize = azureTableConfig.Value.MaxResultPageSize;
    }

    /// <inheritdoc/>
    public List<PostEntity> GetPublicPosts(int pageSize, int pageNumber)
    {
        pageSize = pageSize > 20 || pageSize < 1 ? maxResultPageSize : pageSize;
        int skipCount = (pageNumber - 1) * pageSize;
        return tableClient.Query<PostEntity>(e => e.Display.Equals(true)).OrderByDescending(e => e.CreatedAt).Skip(skipCount).Take(pageSize).ToList();
    }

    /// <inheritdoc/>
    public List<PostEntity> GetPosts(string partitionKey, int pageSize, int pageNumber)
    {
        pageSize = pageSize > 20 || pageSize < 1 ? maxResultPageSize : pageSize;
        int skipCount = (pageNumber - 1) * pageSize;
        return tableClient.Query<PostEntity>(e => e.PartitionKey.Equals(partitionKey, StringComparison.Ordinal) && e.Display.Equals(true)).OrderByDescending(e => e.CreatedAt).Skip(skipCount).Take(pageSize).ToList();
    }

    /// <inheritdoc/>
    public PostEntity GetPost(string rowKey)
    {
        return tableClient.Query<PostEntity>(e => e.RowKey.Equals(rowKey, StringComparison.Ordinal) && e.Display.Equals(true)).FirstOrDefault();
    }

    /// <inheritdoc/>
    public bool CreatePost(PostEntity postEntity)
    {
        Response response = tableClient.AddEntity(postEntity);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }

    /// <inheritdoc/>
    public bool UpdatePost(PostEntity postEntity)
    {
        Response response = tableClient.UpdateEntity(postEntity, ETag.All, TableUpdateMode.Merge);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }

    /// <inheritdoc/>
    public bool DeletePost(string partitionKey, string rowKey)
    {
        Response response = tableClient.DeleteEntity(partitionKey, rowKey);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }

    public List<PostEntity> GetPosts(int partitionKey, int pageSize, int pageNumber)
    {
        throw new NotImplementedException();
    }
}
