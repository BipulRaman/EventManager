using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Basic.Utilities;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using System.Net;

namespace EventManager.App.Api.Extended.Services;

public class PostHandler : IPostHandler
{
    private readonly IPostRepository postRepository;
    private readonly ILogger<PostHandler> logger;

    public PostHandler(IPostRepository postRepository, ILogger<PostHandler> logger)
    {
        this.postRepository = postRepository;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public OpResult<List<PostData>> GetPublicPosts(HttpContext httpContext, int pageSize, int pageNumber)
    {
        logger.LogInformation($"{nameof(PostHandler)}.{nameof(GetPublicPosts)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<List<PostData>> opResult = new OpResult<List<PostData>>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            List<PostEntity> postEntities = postRepository.GetPublicPosts(pageSize, pageNumber);
            List<PostData> posts = ConvertCollection(postEntities);
            opResult.Result = posts;
            opResult.Status = HttpStatusCode.OK;
            opResult.ErrorCode = ErrorCode.None;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(PostHandler)}.{nameof(GetPublicPosts)} => Error occurred while fetching posts for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }

        logger.LogInformation($"{nameof(PostHandler)}.{nameof(GetPublicPosts)} => Method completed for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<List<PostData>> GetPosts(HttpContext httpContext, int pageSize, int pageNumber)
    {
        logger.LogInformation($"{nameof(PostHandler)}.{nameof(GetPublicPosts)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<List<PostData>> opResult = new OpResult<List<PostData>>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            User contextUserInfo = (User)httpContext.Items[NameConstants.USER_KEY];
            List<PostEntity> postEntities = postRepository.GetPosts(contextUserInfo.Id, pageSize, pageNumber);
            List<PostData> posts = ConvertCollection(postEntities);
            opResult.Result = posts;
            opResult.Status = HttpStatusCode.OK;
            opResult.ErrorCode = ErrorCode.None;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(PostHandler)}.{nameof(GetPublicPosts)} => Error occurred while fetching posts for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }

        logger.LogInformation($"{nameof(PostHandler)}.{nameof(GetPublicPosts)} => Method completed for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<PostData> GetPost(HttpContext httpContext, string postId)
    {
        logger.LogInformation($"{nameof(PostHandler)}.{nameof(GetPost)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<PostData> opResult = new OpResult<PostData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (!string.IsNullOrEmpty(postId))
            {
                PostEntity postEntity = postRepository.GetPost(postId);
                if (postEntity is not null)
                {
                    opResult.Result = (PostData)postEntity;
                    opResult.Status = HttpStatusCode.OK;
                    opResult.ErrorCode = ErrorCode.None;
                }
                else
                {
                    opResult.Result = null;
                    opResult.Status = HttpStatusCode.BadRequest;
                    opResult.ErrorCode = ErrorCode.Entity_NotFound;
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
            logger.LogError(ex, $"{nameof(PostHandler)}.{nameof(GetPost)} => Error occurred while fetching post for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }

        logger.LogInformation($"{nameof(PostHandler)}.{nameof(GetPost)} => Method completed for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<PostData> CreatePost(HttpContext httpContext, PostData rawPostData)
    {
        logger.LogInformation($"{nameof(PostHandler)}.{nameof(CreatePost)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<PostData> opResult = new OpResult<PostData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (rawPostData is not null && rawPostData.IsValidToCreate() && httpContext is not null)
            {
                PostEntity postEntity = rawPostData.ConvertToCreateEntity(httpContext);
                bool isSuccessful = postRepository.CreatePost(postEntity);

                if (isSuccessful)
                {
                    opResult.Result = postEntity;
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
            logger.LogError(ex, $"{nameof(PostHandler)}.{nameof(CreatePost)} => Error occurred while creating post for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }

        logger.LogInformation($"{nameof(PostHandler)}.{nameof(CreatePost)} => Method completed for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<PostData> UpdatePost(HttpContext httpContext, PostData postData)
    {
        logger.LogInformation($"{nameof(PostHandler)}.{nameof(UpdatePost)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<PostData> opResult = new OpResult<PostData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (postData is not null && postData.IsValidToUpdate() && httpContext is not null)
            {
                PostEntity postEntity = postData.ConvertToUpdateEntity(httpContext);
                bool isSuccessful = postRepository.UpdatePost(postEntity);

                if (isSuccessful)
                {
                    opResult.Result = postEntity;
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
            logger.LogError(ex, $"{nameof(PostHandler)}.{nameof(UpdatePost)} => Error occurred while updating post for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<bool> DeletePost(HttpContext httpContext, string postId)
    {
        logger.LogInformation($"{nameof(PostHandler)}.{nameof(DeletePost)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<bool> opResult = new OpResult<bool>()
        {
            Result = false,
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (string.IsNullOrEmpty(postId) && httpContext is not null)
            {
                User contextUserInfo = (User)httpContext.Items[NameConstants.USER_KEY];
                bool isSuccessful = postRepository.DeletePost(contextUserInfo.Id, postId);

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
            logger.LogError(ex, $"{nameof(PostHandler)}.{nameof(DeletePost)} => Error occurred while deleting post for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }
        return opResult;
    }

    private List<PostData> ConvertCollection(List<PostEntity> postEntities)
    {
        List<PostData> posts = new List<PostData>();

        if (postEntities is not null && postEntities.Count > 0)
        {
            foreach (PostEntity postEntity in postEntities)
            {
                posts.Add((PostData)postEntity);
            }
        }

        return posts;

    }
}
