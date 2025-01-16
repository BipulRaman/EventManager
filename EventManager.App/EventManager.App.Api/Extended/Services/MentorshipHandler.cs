using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Basic.Utilities;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using System.Net;

namespace EventManager.App.Api.Extended.Services;

public class MentorshipHandler : IMentorshipHandler
{
    private readonly IMentorshipRepository mentorshipRepository;
    private readonly ILogger<MentorshipHandler> logger;

    public MentorshipHandler(IMentorshipRepository mentorshipRepository, ILogger<MentorshipHandler> logger)
    {
        this.mentorshipRepository = mentorshipRepository;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public OpResult<List<MentorshipData>> GetMentorships(HttpContext httpContext)
    {
        logger.LogInformation($"{nameof(MentorshipHandler)}.{nameof(GetMentorships)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<List<MentorshipData>> opResult = new OpResult<List<MentorshipData>>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            List<MentorshipEntity> mentorshipEntities = mentorshipRepository.GetMentorships();
            List<MentorshipData> mentorships = ConvertCollection(mentorshipEntities);
            opResult.Result = mentorships;
            opResult.Status = HttpStatusCode.OK;
            opResult.ErrorCode = ErrorCode.None;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(MentorshipHandler)}.{nameof(GetMentorships)} => Error occurred while fetching mentorships for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }

        logger.LogInformation($"{nameof(MentorshipHandler)}.{nameof(GetMentorships)} => Method completed for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<MentorshipData> GetMentorship(HttpContext httpContext, string mentorshipId)
    {
        logger.LogInformation($"{nameof(MentorshipHandler)}.{nameof(GetMentorship)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<MentorshipData> opResult = new OpResult<MentorshipData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (!string.IsNullOrEmpty(mentorshipId))
            {
                MentorshipData mentorshipData = (MentorshipData)mentorshipRepository.GetMentorship(mentorshipId);
                opResult.Result = mentorshipData;
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
            logger.LogError(ex, $"{nameof(MentorshipHandler)}.{nameof(GetMentorship)} => Error occurred while fetching mentorship for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }

        logger.LogInformation($"{nameof(MentorshipHandler)}.{nameof(GetMentorship)} => Method completed for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<MentorshipData> CreateMentorship(HttpContext httpContext, MentorshipData rawMentorshipData)
    {
        logger.LogInformation($"{nameof(MentorshipHandler)}.{nameof(CreateMentorship)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<MentorshipData> opResult = new OpResult<MentorshipData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (rawMentorshipData is not null && rawMentorshipData.IsValidToCreate() && httpContext is not null)
            {
                MentorshipEntity mentorshipEntity = rawMentorshipData.ConvertToCreateEntity(httpContext);
                bool isSuccessful = mentorshipRepository.CreateMentorship(mentorshipEntity);

                if (isSuccessful)
                {
                    opResult.Result = mentorshipEntity;
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
            logger.LogError(ex, $"{nameof(MentorshipHandler)}.{nameof(CreateMentorship)} => Error occurred while creating mentorship for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }

        logger.LogInformation($"{nameof(MentorshipHandler)}.{nameof(CreateMentorship)} => Method completed for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<MentorshipData> UpdateMentorship(HttpContext httpContext, MentorshipData mentorshipData)
    {
        logger.LogInformation($"{nameof(MentorshipHandler)}.{nameof(UpdateMentorship)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<MentorshipData> opResult = new OpResult<MentorshipData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (mentorshipData is not null && mentorshipData.IsValidToUpdate() && httpContext is not null)
            {
                MentorshipEntity mentorshipEntity = mentorshipData.ConvertToUpdateEntity(httpContext);
                bool isSuccessful = mentorshipRepository.UpdateMentorship(mentorshipEntity);

                if (isSuccessful)
                {
                    opResult.Result = mentorshipEntity;
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
            logger.LogError(ex, $"{nameof(MentorshipHandler)}.{nameof(UpdateMentorship)} => Error occurred while updating mentorship for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<bool> DeleteMentorship(HttpContext httpContext, string mentorshipId)
    {
        logger.LogInformation($"{nameof(MentorshipHandler)}.{nameof(DeleteMentorship)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<bool> opResult = new OpResult<bool>()
        {
            Result = false,
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (string.IsNullOrEmpty(mentorshipId) && httpContext is not null)
            {
                UserEntity contextUserInfo = (UserEntity)httpContext.Items[NameConstants.USER_KEY];
                bool isSuccessful = mentorshipRepository.DeleteMentorship(contextUserInfo.RowKey, mentorshipId);

                if (isSuccessful)
                {
                    opResult.Result = true;
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
            logger.LogError(ex, $"{nameof(MentorshipHandler)}.{nameof(DeleteMentorship)} => Error occurred while deleting mentorship for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }
        return opResult;
    }

    private List<MentorshipData> ConvertCollection(List<MentorshipEntity> mentorshipEntities)
    {
        List<MentorshipData> mentorships = new List<MentorshipData>();

        if (mentorshipEntities is not null && mentorshipEntities.Count > 0)
        {
            foreach (MentorshipEntity mentorshipEntity in mentorshipEntities)
            {
                mentorships.Add((MentorshipData)mentorshipEntity);
            }
        }

        return mentorships;

    }
}
