namespace EventManager.App.Api.Basic.Services;

using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Interfaces;
using EventManager.App.Api.Basic.Models;
using Microsoft.AspNetCore.Http;
using System.Net;

public class UserHandler : IUserHandler
{
    private readonly string tenantId = NameConstants.USER_SERVICE_PARTITION_KEY;
    private readonly IUserRepository userRepository;
    private readonly ILogger<UserHandler> logger;
    public UserHandler(IUserRepository userRepository, ILogger<UserHandler> logger)
    {
        this.userRepository = userRepository;
        this.logger = logger;
    }

    public OpResult<User> HandleGetUser(string id)
    {
        logger.LogInformation($"{nameof(AuthHandler)}.{nameof(HandleGetUser)} => Method started for {id}.");
        OpResult<User> result = new OpResult<User>
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
            Result = null
        };

        try
        {
            User user = userRepository.GetById(id);
            if (user != null)
            {
                result.Status = HttpStatusCode.OK;
                result.ErrorCode = 0;
                result.Result = user;
            }
            else
            {
                result.Status = HttpStatusCode.NotFound;
                result.ErrorCode = ErrorCode.Entity_NotFound;
            }
        }
        catch (Exception)
        {
            logger.LogError($"{nameof(AuthHandler)}.{nameof(HandleGetUser)} => An error occurred while fetching user data for {id}.");
        }

        logger.LogInformation($"{nameof(AuthHandler)}.{nameof(HandleGetUser)} => Method completed for {id}.");
        return result;
    }

    public OpResult<bool> HandleCreateUser(UserCreate userCreate)
    {
        logger.LogInformation($"{nameof(AuthHandler)}.{nameof(HandleCreateUser)} => Method started for {userCreate.Email}.");
        OpResult<bool> result = new OpResult<bool>
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
            Result = false
        };

        try
        {
            bool isExistingUser = userRepository.DoesExist(userCreate.Email);
            if (isExistingUser)
            {
                result.Status = HttpStatusCode.Conflict;
                result.ErrorCode = ErrorCode.Entity_AlreadyExist;
            }
            else
            {
                bool response = userRepository.Create((UserEntity)userCreate);
                if (response is true)
                {
                    result.Status = HttpStatusCode.OK;
                    result.ErrorCode = 0;
                    result.Result = response;
                }
                else
                {
                    result.Status = HttpStatusCode.BadRequest;
                    result.ErrorCode = ErrorCode.Common_BadRequest;
                }
            }
        }
        catch (Exception)
        {
            logger.LogError($"{nameof(AuthHandler)}.{nameof(HandleCreateUser)} => An error occurred while creating user data for {userCreate.Email}.");
        }

        logger.LogInformation($"{nameof(AuthHandler)}.{nameof(HandleCreateUser)} => Method completed for {userCreate.Email}.");
        return result;
    }

    public OpResult<bool> HandleUpdateUser(HttpContext httpContext, UserUpdate userUpdate)
    {
        logger.LogInformation($"{nameof(AuthHandler)}.{nameof(HandleUpdateUser)} => Method started for {userUpdate.Email}.");
        OpResult<bool> result = new OpResult<bool>
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
            Result = false
        };

        try
        {
            UserEntity userEntity = userUpdate.ToUserEntity(httpContext);
            bool response = userRepository.Update(userEntity);
            if (response is true)
            {
                result.Status = HttpStatusCode.OK;
                result.ErrorCode = 0;
                result.Result = response;
            }
            else
            {
                result.Status = HttpStatusCode.BadRequest;
                result.ErrorCode = ErrorCode.Common_BadRequest;
            }
        }
        catch (Exception)
        {
            logger.LogError($"{nameof(AuthHandler)}.{nameof(HandleUpdateUser)} => An error occurred while updating user data for {userUpdate.Email}.");
        }

        logger.LogInformation($"{nameof(AuthHandler)}.{nameof(HandleUpdateUser)} => Method completed for {userUpdate.Email}.");
        return result;
    }

    public OpResult<bool> HandleDeleteUser(string id)
    {
        logger.LogInformation($"{nameof(AuthHandler)}.{nameof(HandleDeleteUser)} => Method started for {id}.");
        OpResult<bool> result = new OpResult<bool>
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
            Result = false
        };

        try
        {
            bool response = userRepository.Delete(tenantId, id);
            if (response is true)
            {
                result.Status = HttpStatusCode.OK;
                result.ErrorCode = 0;
                result.Result = response;
            }
            else
            {
                result.Status = HttpStatusCode.BadRequest;
                result.ErrorCode = ErrorCode.Common_BadRequest;
            }
        }
        catch (Exception)
        {
            logger.LogError($"{nameof(AuthHandler)}.{nameof(HandleDeleteUser)} => An error occurred while deleting user data for {id}.");
        }

        logger.LogInformation($"{nameof(AuthHandler)}.{nameof(HandleDeleteUser)} => Method completed for {id}.");
        return result;
    }
}
