using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Extended.Models;

namespace EventManager.App.Api.Extended.Interfaces;

public interface IBusinessHandler
{
    /// <summary>
    /// Get all businesses.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <returns></returns>
    OpResult<List<BusinessData>> GetBusinesses(HttpContext httpContext);

    /// <summary>
    /// Get all businesses by pin code.
    /// </summary>
    /// <param name="pinCode">PIN Code.</param>
    /// <returns></returns>
    OpResult<List<BusinessData>> GetBusinesses(int pinCode);

    /// <summary>
    /// Get all businesses by pin code and category.
    /// </summary>
    /// <param name="pinCode"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    OpResult<List<BusinessData>> GetBusinesses(int pinCode, string category);

    /// <summary>
    /// Get business by id.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="businessId">Business identity</param>
    /// <returns></returns>
    OpResult<BusinessData> GetBusiness(HttpContext httpContext, string businessId);

    /// <summary>
    /// Create business.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="rawBusinessData">Raw business creation entity.</param>
    /// <returns></returns>
    OpResult<BusinessData> CreateBusiness(HttpContext httpContext, BusinessData rawBusinessData);

    /// <summary>
    /// Update business.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="businessData">Business entity.</param>
    /// <returns></returns>
    OpResult<BusinessData> UpdateBusiness(HttpContext httpContext, BusinessData businessData);

    /// <summary>
    /// Delete business.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="businessId">Business entity.</param>
    /// <returns></returns>
    OpResult<BusinessData> DeleteBusiness(HttpContext httpContext, string businessId);
}
