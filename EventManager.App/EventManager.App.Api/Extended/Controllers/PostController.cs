using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Basic.Utilities;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EventManager.App.Api.Extended.Controllers;

[Route("post")]
[ApiController]
public class PostController : Controller
{
    private readonly IPostHandler postHandler;
    private readonly ILogger<ProfileController> logger;

    public PostController(IPostHandler postHandler, ILogger<ProfileController> logger)
    {
        this.postHandler = postHandler;
        this.logger = logger;
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpGet("public/{pageSize}/{pageNumber}")]
    [ProducesResponseType(typeof(OpResult<List<PostData>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<List<PostData>>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<List<PostData>>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult GetPublicPosts(int pageSize, int pageNumber)
    {
        logger.LogInformation($"{nameof(PostController)}.{nameof(GetPosts)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<List<PostData>> opResult = postHandler.GetPublicPosts(HttpContext, pageSize, pageNumber);
        logger.LogInformation($"{nameof(PostController)}.{nameof(GetPosts)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpGet("{pageSize}/{pageNumber}")]
    [ProducesResponseType(typeof(OpResult<List<PostData>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<List<PostData>>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<List<PostData>>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult GetPosts(int pageSize, int pageNumber)
    {
        logger.LogInformation($"{nameof(PostController)}.{nameof(GetPosts)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<List<PostData>> opResult = postHandler.GetPosts(HttpContext, pageSize, pageNumber);
        logger.LogInformation($"{nameof(PostController)}.{nameof(GetPosts)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpGet("{postId}")]
    [ProducesResponseType(typeof(OpResult<PostData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<PostData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<PostData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<PostData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult GetPost(string postId)
    {
        logger.LogInformation($"{nameof(PostController)}.{nameof(GetPost)} => Started by User:  {ContextHelper.GetLoggedInUser(HttpContext)?.Id} .");
        OpResult<PostData> opResult = postHandler.GetPost(HttpContext, postId);
        logger.LogInformation($"{nameof(PostController)}.{nameof(GetPost)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpPost]
    [ProducesResponseType(typeof(OpResult<PostData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<PostData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<PostData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<PostData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult CreatePost([FromBody] PostDataCreate postData)
    {
        logger.LogInformation($"{nameof(PostController)}.{nameof(CreatePost)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<PostData> opResult = postHandler.CreatePost(HttpContext, postData);
        logger.LogInformation($"{nameof(PostController)}.{nameof(CreatePost)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpPatch]
    [ProducesResponseType(typeof(OpResult<PostData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<PostData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<PostData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<PostData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult UpdatePost([FromBody] PostDataUpdate postData)
    {
        logger.LogInformation($"{nameof(PostController)}.{nameof(UpdatePost)} => Started by User:  {ContextHelper.GetLoggedInUser(HttpContext)?.Id} .");
        OpResult<PostData> opResult = postHandler.UpdatePost(HttpContext, postData);
        logger.LogInformation($"{nameof(PostController)}.{nameof(UpdatePost)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpDelete("{postId}")]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult DeletePost(string postId)
    {
        logger.LogInformation($"{nameof(PostController)}.{nameof(DeletePost)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<bool> opResult = postHandler.DeletePost(HttpContext, postId);
        logger.LogInformation($"{nameof(PostController)}.{nameof(DeletePost)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }
}
