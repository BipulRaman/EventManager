using EventManager.App.Api.Extended.Models;

namespace EventManager.App.Api.Extended.Interfaces;

public interface IPostRepository
{
    /// <summary>
    /// Get all public posts.
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    List<PostEntity> GetPublicPosts(int pageSize, int pageNumber);

    /// <summary>
    /// Get all posts.
    /// </summary>
    /// <param name="partitionKey"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    List<PostEntity> GetPosts(string partitionKey, int pageSize, int pageNumber);

    /// <summary>
    /// Get post by id.
    /// </summary>
    /// <param name="rowKey">Post identity.</param>
    /// <returns></returns>
    PostEntity GetPost(string rowKey);

    /// <summary>
    /// Create post.
    /// </summary>
    /// <param name="postEntity">Post entity.</param>
    /// <returns></returns>
    bool CreatePost(PostEntity postEntity);

    /// <summary>
    /// Update post.
    /// </summary>
    /// <param name="postEntity">Post entity.</param>
    /// <returns></returns>
    bool UpdatePost(PostEntity postEntity);

    /// <summary>
    /// Delete post.
    /// </summary>
    /// <param name="partitionKey">Post partition key.</param>
    /// <param name="rowKey">Post row key.</param>
    /// <returns></returns>
    bool DeletePost(string partitionKey, string rowKey);
}
