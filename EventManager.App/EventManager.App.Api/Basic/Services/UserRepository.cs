namespace EventManager.App.Api.Basic.Services;

using Azure;
using Azure.Data.Tables;
using EventManager.App.Api.Basic.Interfaces;
using EventManager.App.Api.Basic.Models;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

/// <summary>
/// The <see cref="UserRepository"/> class represents a repository for managing users.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly TableClient tableClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository"/> class.
    /// </summary>
    /// <param name="azureTableConfig">The Azure Table configuration options.</param>
    public UserRepository(IOptions<AzureTableConfig> azureTableConfig)
    {
        TableServiceClient tableServiceClient = new TableServiceClient(azureTableConfig.Value.ConnectionString);
        tableClient = tableServiceClient.GetTableClient(azureTableConfig.Value.UserTable);
    }

    ///<inheritdoc/>
    public bool Create(UserEntity userEntity)
    {
        var response = tableClient.AddEntity(userEntity);
        if (response.Status == 204)
        {
            return true;
        }
        return false;
    }

    ///<inheritdoc/>
    public bool Delete(string tenantId, string id)
    {
        Response response = tableClient.DeleteEntity(tenantId, id);
        if (response.Status == 204)
        {
            return true;
        }
        return false;
    }

    ///<inheritdoc/>
    public bool DoesExist(string email)
    {
        User responseUser = tableClient.Query<UserEntity>(e => e.Email.Equals(email, StringComparison.Ordinal)).SingleOrDefault();
        return responseUser != null;
    }

    ///<inheritdoc/>
    public User GetByEmail(string email)
    {
        User responseUser = tableClient.Query<UserEntity>(e => e.Email.Equals(email, StringComparison.Ordinal)).SingleOrDefault();
        return responseUser;
    }

    ///<inheritdoc/>
    public User GetById(string id)
    {
        User responseUser = tableClient.Query<UserEntity>(e => e.RowKey.Equals(id, StringComparison.Ordinal)).SingleOrDefault();
        return responseUser;
    }

    ///<inheritdoc/>
    public bool Update(UserEntity userEntity)
    {
        Response response = tableClient.UpdateEntity(userEntity, ETag.All, TableUpdateMode.Merge);
        if (response.Status == 204)
        {
            return true;
        }
        return false;
    }
}
