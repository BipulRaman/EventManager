using Azure;
using Azure.Data.Tables;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using Microsoft.Extensions.Options;

namespace EventManager.App.Api.Extended.Services;

public class BusinessRepository : IBusinessRepository
{
    private readonly TableClient tableClient;

    public BusinessRepository(IOptions<AzureTableConfig> azureTableConfig)
    {
        TableServiceClient tableServiceClient = new TableServiceClient(azureTableConfig.Value.ConnectionString);
        tableClient = tableServiceClient.GetTableClient(azureTableConfig.Value.BusinessesTable);
    }

    /// <inheritdoc/>
    public BusinessEntity GetBusiness(string rowKey)
    {
        return tableClient.Query<BusinessEntity>(e => e.RowKey.Equals(rowKey, StringComparison.Ordinal)).FirstOrDefault();
    }

    /// <inheritdoc/>
    public List<BusinessEntity> GetBusinessesByUser(string userId)
    {
        return tableClient.Query<BusinessEntity>(e => e.PartitionKey.Equals(userId, StringComparison.Ordinal)).OrderByDescending(e => e.Timestamp).ToList();
    }

    /// <inheritdoc/>
    public List<BusinessEntity> GetBusinesses()
    {
        return tableClient.Query<BusinessEntity>().OrderByDescending(e => e.Timestamp).ToList();
    }

    /// <inheritdoc/>
    public List<BusinessEntity> GetBusinesses(int pinCode)
    {
        return tableClient.Query<BusinessEntity>(e => e.PinCode == pinCode).OrderByDescending(e => e.Timestamp).ToList();
    }

    /// <inheritdoc/>
    public List<BusinessEntity> GetBusinesses(int pinCode, string category)
    {
        return tableClient.Query<BusinessEntity>(e => e.PinCode == pinCode && e.Category.Equals(category, StringComparison.InvariantCultureIgnoreCase)).OrderByDescending(e => e.Timestamp).ToList();
    }

    /// <inheritdoc/>
    public bool CreateBusiness(BusinessEntity businessEntity)
    {
        Response response = tableClient.AddEntity(businessEntity);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }

    /// <inheritdoc/>
    public bool UpdateBusiness(BusinessEntity businessEntity)
    {
        Response response = tableClient.UpdateEntity(businessEntity, ETag.All, TableUpdateMode.Merge);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }

    /// <inheritdoc/>
    public bool DeleteBusiness(string partitionKey, string rowKey)
    {
        Response response = tableClient.DeleteEntity(partitionKey, rowKey);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }
}
