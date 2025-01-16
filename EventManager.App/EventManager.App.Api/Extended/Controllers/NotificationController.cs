using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Basic.Utilities;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EventManager.App.Api.Extended.Controllers;

[Route("notification")]
[ApiController]
public class NotificationController : Controller
{
    private readonly INotificationHandler notificationHandler;
    private readonly ILogger<ProfileController> logger;

    public NotificationController(INotificationHandler notificationHandler, ILogger<ProfileController> logger)
    {
        this.notificationHandler = notificationHandler;
        this.logger = logger;
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpGet]
    [ProducesResponseType(typeof(OpResult<List<NotificationData>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<List<NotificationData>>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<List<NotificationData>>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult GetNotifications()
    {
        logger.LogInformation($"{nameof(NotificationController)}.{nameof(GetNotifications)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<List<NotificationData>> opResult = notificationHandler.GetNotifications(HttpContext);
        logger.LogInformation($"{nameof(NotificationController)}.{nameof(GetNotifications)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpGet("{notificationId}")]
    [ProducesResponseType(typeof(OpResult<NotificationData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<NotificationData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<NotificationData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<NotificationData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult GetNotification(string notificationId)
    {
        logger.LogInformation($"{nameof(NotificationController)}.{nameof(GetNotification)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<NotificationData> opResult = notificationHandler.GetNotification(HttpContext, notificationId);
        logger.LogInformation($"{nameof(NotificationController)}.{nameof(GetNotification)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.Admin))]
    [HttpPost]
    [ProducesResponseType(typeof(OpResult<NotificationData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<NotificationData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<NotificationData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<NotificationData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult CreateNotification([FromBody] NotificationDataCreate notificationEntity)
    {
        logger.LogInformation($"{nameof(NotificationController)}.{nameof(CreateNotification)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<NotificationData> opResult = notificationHandler.CreateNotification(HttpContext, notificationEntity);
        logger.LogInformation($"{nameof(NotificationController)}.{nameof(CreateNotification)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.Admin))]
    [HttpPatch]
    [ProducesResponseType(typeof(OpResult<NotificationData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<NotificationData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<NotificationData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<NotificationData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult UpdateNotification([FromBody] NotificationDataUpdate notificationEntity)
    {
        logger.LogInformation($"{nameof(NotificationController)}.{nameof(UpdateNotification)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<NotificationData> opResult = notificationHandler.UpdateNotification(HttpContext, notificationEntity);
        logger.LogInformation($"{nameof(NotificationController)}.{nameof(UpdateNotification)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.Admin))]
    [HttpDelete("{notificationId}")]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult DeleteNotification(string notificationId)
    {
        logger.LogInformation($"{nameof(NotificationController)}.{nameof(DeleteNotification)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<bool> opResult = notificationHandler.DeleteNotification(HttpContext, notificationId);
        logger.LogInformation($"{nameof(NotificationController)}.{nameof(DeleteNotification)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }
}
