using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Basic.Utilities;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using System.Net;

namespace EventManager.App.Api.Extended.Services;

public class BusinessHandler : IBusinessHandler
{
    private readonly IBusinessRepository businessRepository;
    private readonly ILogger<BusinessHandler> logger;

    public BusinessHandler(IBusinessRepository businessRepository, ILogger<BusinessHandler> logger)
    {
        this.businessRepository = businessRepository;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public OpResult<List<BusinessData>> GetBusinesses(HttpContext httpContext)
    {
        logger.LogInformation($"{nameof(BusinessHandler)}.{nameof(GetBusinesses)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<List<BusinessData>> opResult = new OpResult<List<BusinessData>>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            User contextUserInfo = (User)httpContext.Items[NameConstants.USER_KEY];
            List<BusinessEntity> businessEntities = businessRepository.GetBusinessesByUser(contextUserInfo.Id);
            List<BusinessData> businessesData = ConvertCollection(businessEntities);
            opResult.Result = businessesData;
            opResult.Status = HttpStatusCode.OK;
            opResult.ErrorCode = ErrorCode.None;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(BusinessHandler)}.{nameof(GetBusinesses)} => Error occurred while fetching businesses for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }

        logger.LogInformation($"{nameof(BusinessHandler)}.{nameof(GetBusinesses)} => Method completed for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<List<BusinessData>> GetBusinesses(int pinCode)
    {
        logger.LogInformation($"{nameof(BusinessHandler)}.{nameof(GetBusinesses)} => Method started for PIN:{pinCode}");
        OpResult<List<BusinessData>> opResult = new OpResult<List<BusinessData>>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (pinCode >= 100000 && pinCode <= 999999)
            {
                List<BusinessEntity> businessEntities = businessRepository.GetBusinesses(pinCode);
                List<BusinessData> businessesData = ConvertCollection(businessEntities);
                opResult.Result = businessesData;
                opResult.Status = HttpStatusCode.OK;
                opResult.ErrorCode = ErrorCode.None;
            }
            else
            {
                opResult.Status = HttpStatusCode.BadRequest;
                opResult.ErrorCode = ErrorCode.Common_BadRequest;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(BusinessHandler)}.{nameof(GetBusinesses)} => Error occurred while fetching businesses for PIN:{pinCode}");
        }

        logger.LogInformation($"{nameof(BusinessHandler)}.{nameof(GetBusinesses)} => Method completed for PIN:{pinCode}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<List<BusinessData>> GetBusinesses(int pinCode, string category)
    {
        logger.LogInformation($"{nameof(BusinessHandler)}.{nameof(GetBusinesses)} => Method started for PIN:{pinCode} and Category:{category}");
        OpResult<List<BusinessData>> opResult = new OpResult<List<BusinessData>>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (!string.IsNullOrWhiteSpace(category) && pinCode >= 100000 && pinCode <= 999999)
            {
                List<BusinessEntity> businessEntities = businessRepository.GetBusinesses(pinCode, category);
                List<BusinessData> businessesData = ConvertCollection(businessEntities);
                opResult.Result = businessesData;
                opResult.Status = HttpStatusCode.OK;
                opResult.ErrorCode = ErrorCode.None;
            }
            else
            {
                opResult.Status = HttpStatusCode.BadRequest;
                opResult.ErrorCode = ErrorCode.Common_BadRequest;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(BusinessHandler)}.{nameof(GetBusinesses)} => Error occurred while fetching businesses for PIN:{pinCode} and Category:{category}");
        }

        logger.LogInformation($"{nameof(BusinessHandler)}.{nameof(GetBusinesses)} => Method completed for PIN:{pinCode} and Category:{category}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<BusinessData> GetBusiness(HttpContext httpContext, string businessId)
    {
        logger.LogInformation($"{nameof(BusinessHandler)}.{nameof(GetBusiness)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<BusinessData> opResult = new OpResult<BusinessData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (!string.IsNullOrEmpty(businessId))
            {
                BusinessData businessEntity = businessRepository.GetBusiness(businessId);
                opResult.Result = businessEntity;
                opResult.Status = HttpStatusCode.OK;
                opResult.ErrorCode = ErrorCode.None;
            }
            else
            {
                opResult.Status = HttpStatusCode.BadRequest;
                opResult.ErrorCode = ErrorCode.Common_BadRequest;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(BusinessHandler)}.{nameof(GetBusiness)} => Error occurred while fetching business for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }

        logger.LogInformation($"{nameof(BusinessHandler)}.{nameof(GetBusiness)} => Method completed for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<BusinessData> CreateBusiness(HttpContext httpContext, BusinessData rawBusinessData)
    {
        logger.LogInformation($"{nameof(BusinessHandler)}.{nameof(CreateBusiness)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<BusinessData> opResult = new OpResult<BusinessData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (rawBusinessData is not null && rawBusinessData.IsValidToCreate() && httpContext is not null)
            {
                BusinessEntity businessEntity = rawBusinessData.ConvertToCreateEntity(httpContext);
                bool isSuccessful = businessRepository.CreateBusiness(businessEntity);

                if (isSuccessful)
                {
                    opResult.Result = businessEntity;
                    opResult.Status = HttpStatusCode.OK;
                    opResult.ErrorCode = ErrorCode.None;
                }
                else
                {
                    opResult.Status = HttpStatusCode.InternalServerError;
                    opResult.ErrorCode = ErrorCode.Entity_Create_Failed;
                }
            }
            else
            {
                opResult.Status = HttpStatusCode.BadRequest;
                opResult.ErrorCode = ErrorCode.Common_BadRequest;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(BusinessHandler)}.{nameof(CreateBusiness)} => Error occurred while creating business for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }

        logger.LogInformation($"{nameof(BusinessHandler)}.{nameof(CreateBusiness)} => Method completed for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<BusinessData> UpdateBusiness(HttpContext httpContext, BusinessData businessData)
    {
        logger.LogInformation($"{nameof(BusinessHandler)}.{nameof(UpdateBusiness)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<BusinessData> opResult = new OpResult<BusinessData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (businessData is not null && businessData.IsValidToUpdate() && httpContext is not null)
            {
                User contextUserInfo = (User)httpContext.Items[NameConstants.USER_KEY];
                BusinessEntity businessFromDb = businessRepository.GetBusiness(businessData.Id);
                if (businessFromDb is null || businessFromDb.PartitionKey != contextUserInfo.Id)
                {
                    opResult.Status = HttpStatusCode.BadRequest;
                    opResult.ErrorCode = ErrorCode.Entity_NotFound;
                    return opResult;
                }
                BusinessEntity businessEntity = businessData.ConvertToUpdateEntity(httpContext);
                bool isSuccessful = businessRepository.UpdateBusiness(businessEntity);

                if (isSuccessful)
                {
                    opResult.Result = businessEntity;
                    opResult.Status = HttpStatusCode.OK;
                    opResult.ErrorCode = ErrorCode.None;
                }
                else
                {
                    opResult.Status = HttpStatusCode.InternalServerError;
                    opResult.ErrorCode = ErrorCode.Entity_Update_Failed;
                }
            }
            else
            {
                opResult.Status = HttpStatusCode.BadRequest;
                opResult.ErrorCode = ErrorCode.Common_BadRequest;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(BusinessHandler)}.{nameof(UpdateBusiness)} => Error occurred while updating business for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }
        return opResult;
    }


    /// <inheritdoc/>
    public OpResult<BusinessData> DeleteBusiness(HttpContext httpContext, string businessId)
    {
        logger.LogInformation($"{nameof(BusinessHandler)}.{nameof(DeleteBusiness)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<BusinessData> opResult = new OpResult<BusinessData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (string.IsNullOrEmpty(businessId) && httpContext is not null)
            {
                User contextUserInfo = (User)httpContext.Items[NameConstants.USER_KEY];

                BusinessEntity businessFromDb = businessRepository.GetBusiness(businessId);
                if (businessFromDb is null || businessFromDb.PartitionKey != contextUserInfo.Id)
                {
                    opResult.Status = HttpStatusCode.BadRequest;
                    opResult.ErrorCode = ErrorCode.Entity_NotFound;
                    return opResult;
                }
                bool isSuccessful = businessRepository.DeleteBusiness(contextUserInfo.Id, businessId);

                if (isSuccessful)
                {
                    opResult.Status = HttpStatusCode.OK;
                    opResult.ErrorCode = ErrorCode.None;
                }
                else
                {
                    opResult.Status = HttpStatusCode.InternalServerError;
                    opResult.ErrorCode = ErrorCode.Entity_Delete_Failed;
                }
            }
            else
            {
                opResult.Status = HttpStatusCode.BadRequest;
                opResult.ErrorCode = ErrorCode.Common_BadRequest;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(BusinessHandler)}.{nameof(DeleteBusiness)} => Error occurred while deleting business for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }
        return opResult;
    }

    private List<BusinessData> ConvertCollection(List<BusinessEntity> businessEntities)
    {
        List<BusinessData> posts = new List<BusinessData>();

        if (businessEntities is not null && businessEntities.Count > 0)
        {
            foreach (BusinessEntity postEntity in businessEntities)
            {
                posts.Add((BusinessData)postEntity);
            }
        }

        return posts;

    }
}
