using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Basic.Utilities;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EventManager.App.Api.Extended.Controllers;

[Route("mentorship")]
[ApiController]
public class MentorshipController : Controller
{
    private readonly IMentorshipHandler mentorshipHandler;
    private readonly ILogger<ProfileController> logger;

    public MentorshipController(IMentorshipHandler mentorshipHandler, ILogger<ProfileController> logger)
    {
        this.mentorshipHandler = mentorshipHandler;
        this.logger = logger;
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpGet]
    [ProducesResponseType(typeof(OpResult<List<MentorshipData>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<List<MentorshipData>>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<List<MentorshipData>>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult GetMentorships()
    {
        logger.LogInformation($"{nameof(MentorshipController)}.{nameof(GetMentorships)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<List<MentorshipData>> opResult = mentorshipHandler.GetMentorships(HttpContext);
        logger.LogInformation($"{nameof(MentorshipController)}.{nameof(GetMentorships)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpGet("{mentorshipId}")]
    [ProducesResponseType(typeof(OpResult<MentorshipData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<MentorshipData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<MentorshipData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<MentorshipData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult GetMentorship(string mentorshipId)
    {
        logger.LogInformation($"{nameof(MentorshipController)}.{nameof(GetMentorship)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<MentorshipData> opResult = mentorshipHandler.GetMentorship(HttpContext, mentorshipId);
        logger.LogInformation($"{nameof(MentorshipController)}.{nameof(GetMentorship)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpPost]
    [ProducesResponseType(typeof(OpResult<MentorshipData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<MentorshipData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<MentorshipData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<MentorshipData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult CreateMentorship([FromBody] MentorshipDataCreate mentorshipData)
    {
        logger.LogInformation($"{nameof(MentorshipController)}.{nameof(CreateMentorship)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<MentorshipData> opResult = mentorshipHandler.CreateMentorship(HttpContext, mentorshipData);
        logger.LogInformation($"{nameof(MentorshipController)}.{nameof(CreateMentorship)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpPatch]
    [ProducesResponseType(typeof(OpResult<MentorshipData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<MentorshipData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<MentorshipData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<MentorshipData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult UpdateMentorship([FromBody] MentorshipDataUpdate mentorshipData)
    {
        logger.LogInformation($"{nameof(MentorshipController)}.{nameof(UpdateMentorship)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<MentorshipData> opResult = mentorshipHandler.UpdateMentorship(HttpContext, mentorshipData);
        logger.LogInformation($"{nameof(MentorshipController)}.{nameof(UpdateMentorship)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpDelete("{mentorshipId}")]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult DeleteMentorship(string mentorshipId)
    {
        logger.LogInformation($"{nameof(MentorshipController)}.{nameof(DeleteMentorship)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<bool> opResult = mentorshipHandler.DeleteMentorship(HttpContext, mentorshipId);
        logger.LogInformation($"{nameof(MentorshipController)}.{nameof(DeleteMentorship)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }
}
