using Azure;
using Azure.Data.Tables;
using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using Microsoft.Extensions.Options;

namespace EventManager.App.Api.Extended.Services;

public class ProfileRepository : IProfileRepository
{
    private readonly string PartitionKey = NameConstants.USER_SERVICE_PARTITION_KEY;
    private readonly TableClient tableClient;

    public ProfileRepository(IOptions<AzureTableConfig> azureTableConfig)
    {
        TableServiceClient tableServiceClient = new TableServiceClient(azureTableConfig.Value.ConnectionString);
        tableClient = tableServiceClient.GetTableClient(azureTableConfig.Value.UserTable);
    }

    /// <inheritdoc/>
    public ProfileEntity GetUserDetailsByEmail(string email)
    {
        var response = tableClient.Query<ProfileEntity>(e => e.PartitionKey.Equals(PartitionKey) && e.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
        return response;
    }

    /// <inheritdoc/>
    public ProfileEntity GetUserDetailsById(string id)
    {
        return tableClient.Query<ProfileEntity>(e => e.PartitionKey.Equals(PartitionKey) && e.RowKey.Equals(id, StringComparison.Ordinal)).SingleOrDefault();
    }

    /// <inheritdoc/>
    public List<ProfileEntity> GetUsersInGeoRange(double minLat, double maxLat, double minLon, double maxLon)
    {
        var response = tableClient.Query<ProfileEntity>(e => e.PartitionKey.Equals(PartitionKey) && e.Latitude >= minLat && e.Latitude <= maxLat && e.Longitude >= minLon && e.Longitude <= maxLon).ToList();
        return response;
    }

    public List<ProfileEntity> GetUsersByPhone(string phone)
    {
        var response = tableClient.Query<ProfileEntity>(e => e.PartitionKey.Equals(PartitionKey) && e.Phone.Equals(phone, StringComparison.OrdinalIgnoreCase)).ToList();
        return response;
    }

    /// <inheritdoc/>
    public bool AddUser(ProfileEntity profileEntity)
    {
        profileEntity.PartitionKey = PartitionKey;
        Response response = tableClient.AddEntity(profileEntity);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }

    /// <inheritdoc/>
    public bool DeleteUser(string rowKey)
    {
        Response response = tableClient.DeleteEntity(PartitionKey, rowKey);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }

    /// <inheritdoc/>
    public bool UpdateUserDetails(ProfileEntity profileEntity)
    {
        profileEntity.PartitionKey = PartitionKey;
        Response response = tableClient.UpdateEntity(profileEntity, ETag.All, TableUpdateMode.Merge);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }

    public bool CheckUserExists(string email)
    {
        var response = tableClient.Query<UserEntity>(e => e.PartitionKey.Equals(PartitionKey) && e.Email.Equals(email, StringComparison.Ordinal)).SingleOrDefault();
        if (response != null)
        {
            return true;
        }
        return false;
    }

    public bool VenueCheckIn(string userId)
    {
        ProfileEntity profileEntity = GetUserDetailsById(userId);
        if (profileEntity != null)
        {
            profileEntity.VenueCheckInDateTime = DateTimeOffset.UtcNow;
            return UpdateUserDetails(profileEntity);
        }
        return false;
    }

    public bool GiftCheckIn(string userId)
    {
        ProfileEntity profileEntity = GetUserDetailsById(userId);
        if (profileEntity != null)
        {
            profileEntity.GiftCheckInDateTime = DateTimeOffset.UtcNow;
            return UpdateUserDetails(profileEntity);
        }
        return false;
    }

    public bool MealCheckIn(string userId)
    {
        ProfileEntity profileEntity = GetUserDetailsById(userId);
        if (profileEntity != null)
        {
            profileEntity.MealCheckInDateTime = DateTimeOffset.UtcNow;
            return UpdateUserDetails(profileEntity);
        }
        return false;
    }
}
