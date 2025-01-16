using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Basic.Utilities;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EventManager.App.Api.Extended.Controllers;

[Route("event")]
[ApiController]
public class EventController : Controller
{
    private readonly IEventsHandler eventHandler;
    private readonly ILogger<ProfileController> logger;

    public EventController(IEventsHandler eventHandler, ILogger<ProfileController> logger)
    {
        this.eventHandler = eventHandler;
        this.logger = logger;
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpGet]
    [ProducesResponseType(typeof(OpResult<List<EventData>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<List<EventData>>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<List<EventData>>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult GetEvents()
    {
        logger.LogInformation($"{nameof(EventController)}.{nameof(GetEvents)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<List<EventData>> opResult = eventHandler.GetEvents(HttpContext);
        logger.LogInformation($"{nameof(EventController)}.{nameof(GetEvents)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpGet("{eventId}")]
    [ProducesResponseType(typeof(OpResult<EventData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<EventData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<EventData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<EventData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult GetEvent(string eventId)
    {
        logger.LogInformation($"{nameof(EventController)}.{nameof(GetEvent)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<EventData> opResult = eventHandler.GetEvent(HttpContext, eventId);
        logger.LogInformation($"{nameof(EventController)}.{nameof(GetEvent)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpPost]
    [ProducesResponseType(typeof(OpResult<EventData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<EventData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<EventData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<EventData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult CreateEvent([FromBody] EventDataCreate eventEntity)
    {
        logger.LogInformation($"{nameof(EventController)}.{nameof(CreateEvent)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<EventData> opResult = eventHandler.CreateEvent(HttpContext, eventEntity);
        logger.LogInformation($"{nameof(EventController)}.{nameof(CreateEvent)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpPatch]
    [ProducesResponseType(typeof(OpResult<EventData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<EventData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<EventData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<EventData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult UpdateEvent([FromBody] EventDataUpdate eventEntity)
    {
        logger.LogInformation($"{nameof(EventController)}.{nameof(UpdateEvent)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<EventData> opResult = eventHandler.UpdateEvent(HttpContext, eventEntity);
        logger.LogInformation($"{nameof(EventController)}.{nameof(UpdateEvent)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpDelete("{eventId}")]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult DeleteEvent(string eventId)
    {
        logger.LogInformation($"{nameof(EventController)}.{nameof(DeleteEvent)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<bool> opResult = eventHandler.DeleteEvent(HttpContext, eventId);
        logger.LogInformation($"{nameof(EventController)}.{nameof(DeleteEvent)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }
}
