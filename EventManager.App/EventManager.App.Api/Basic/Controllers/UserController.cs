namespace EventManager.App.Api.Basic.Controllers;

using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Interfaces;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Basic.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

[Route("user")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserHandler userHandler;
    private readonly ILogger<UserController> logger;

    public UserController(IUserHandler userHandler, ILogger<UserController> logger)
    {
        this.userHandler = userHandler;
        this.logger = logger;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = nameof(Role.Admin))]
    [ProducesResponseType(typeof(OpResult<User>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<User>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<User>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(OpResult<User>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult GetUser(string id)
    {
        logger.LogInformation($"{nameof(UserController)}.{nameof(GetUser)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<User> opResult = userHandler.HandleGetUser(id);
        logger.LogInformation($"{nameof(UserController)}.{nameof(GetUser)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [HttpPost]
    [Authorize(Roles = nameof(Role.Admin))]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult CreateUser(UserCreate userCreate)
    {
        logger.LogInformation($"{nameof(UserController)}.{nameof(CreateUser)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<bool> opResult = userHandler.HandleCreateUser(HttpContext, userCreate);
        logger.LogInformation($"{nameof(UserController)}.{nameof(CreateUser)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [HttpPatch]
    [Authorize(Roles = nameof(Role.User))]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult UpdateUser(UserUpdate user)
    {
        logger.LogInformation($"{nameof(UserController)}.{nameof(UpdateUser)} => Started by User:  {ContextHelper.GetLoggedInUser(HttpContext)?.Id} .");
        OpResult<bool> opResult = userHandler.HandleUpdateUser(HttpContext, user);
        logger.LogInformation($"{nameof(UserController)}.{nameof(UpdateUser)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = nameof(Role.Admin))]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult DeleteUser(string id)
    {
        logger.LogInformation($"{nameof(UserController)}.{nameof(DeleteUser)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<bool> opResult = userHandler.HandleDeleteUser(id);
        logger.LogInformation($"{nameof(UserController)}.{nameof(DeleteUser)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }
}
