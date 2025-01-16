using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Extended.Models;

namespace EventManager.App.Api.Extended.Interfaces;

public interface IPostHandler
{
    /// <summary>
    /// Get all public posts.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="pageNumber">Page number.</param>
    /// <returns></returns>
    OpResult<List<PostData>> GetPublicPosts(HttpContext httpContext, int pageSize, int pageNumber);

    /// <summary>
    /// Get all posts.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="pageNumber">Page number.</param>
    /// <returns></returns>
    OpResult<List<PostData>> GetPosts(HttpContext httpContext, int pageSize, int pageNumber);

    /// <summary>
    /// Get post by id.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="postId">Post identity</param>
    /// <returns></returns>
    OpResult<PostData> GetPost(HttpContext httpContext, string postId);

    /// <summary>
    /// Create post.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="rawPostData">Raw post creation data.</param>
    /// <returns></returns>
    OpResult<PostData> CreatePost(HttpContext httpContext, PostData rawPostData);

    /// <summary>
    /// Update post.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="postData">Post data.</param>
    /// <returns></returns>
    OpResult<PostData> UpdatePost(HttpContext httpContext, PostData postData);

    /// <summary>
    /// Delete post.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="postId">Post entity.</param>
    /// <returns></returns>
    OpResult<bool> DeletePost(HttpContext httpContext, string postId);
}
