using EventManager.App.Api.Extended.Models;

namespace EventManager.App.Api.Extended.Interfaces;

public interface IBusinessRepository
{
    /// <summary>
    /// Get business by id.
    /// </summary>
    /// <param name="rowKey">Business identity.</param>
    /// <returns></returns>
    BusinessEntity GetBusiness(string rowKey);

    /// <summary>
    /// Get businesses by user.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    List<BusinessEntity> GetBusinessesByUser(string userId);

    /// <summary>
    /// Get all businesses.
    /// </summary>
    /// <returns></returns>
    List<BusinessEntity> GetBusinesses();

    /// <summary>
    /// Get business by pin code.
    /// </summary>
    /// <param name="pinCode"></param>
    /// <returns></returns>
    List<BusinessEntity> GetBusinesses(int pinCode);

    /// <summary>
    /// Get business by pin code and category.
    /// </summary>
    /// <param name="pinCode"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    List<BusinessEntity> GetBusinesses(int pinCode, string category);

    /// <summary>
    /// Create business.
    /// </summary>
    /// <param name="businessEntity">Business entity.</param>
    /// <returns></returns>
    bool CreateBusiness(BusinessEntity businessEntity);

    /// <summary>
    /// Update business.
    /// </summary>
    /// <param name="businessEntity">Business entity.</param>
    /// <returns></returns>
    bool UpdateBusiness(BusinessEntity businessEntity);

    /// <summary>
    /// Delete business.
    /// </summary>
    /// <param name="partitionKey">Business partition key.</param>
    /// <param name="rowKey">Business row key.</param>
    /// <returns></returns>
    bool DeleteBusiness(string partitionKey, string rowKey);
}
