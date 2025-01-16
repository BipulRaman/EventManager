using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Basic.Utilities;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EventManager.App.Api.Extended.Controllers;

[Route("profile")]
[ApiController]
public class ProfileController : Controller
{
    private readonly IProfileHandler profileHandler;

    private readonly ILogger<ProfileController> logger;

    public ProfileController(IProfileHandler profileHandler, ILogger<ProfileController> logger)
    {
        this.profileHandler = profileHandler;
        this.logger = logger;
    }

    [Authorize(Roles = $"{nameof(Role.User)},{nameof(Role.InvitedUser)}")]
    [HttpGet]
    [ProducesResponseType(typeof(OpResult<ProfileData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<ProfileData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<ProfileData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult GetUserProfile()
    {
        logger.LogInformation($"{nameof(ProfileController)}.{nameof(GetUserProfile)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<ProfileData> opResult = profileHandler.GetUserProfile(HttpContext);
        logger.LogInformation($"{nameof(ProfileController)}.{nameof(GetUserProfile)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(OpResult<ProfileDataPublic>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<ProfileDataPublic>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<ProfileDataPublic>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult GetUserProfileById(string userId)
    {
        logger.LogInformation($"{nameof(ProfileController)}.{nameof(GetUserProfile)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<ProfileDataPublic> opResult = profileHandler.GetUserProfilePublic(userId);
        logger.LogInformation($"{nameof(ProfileController)}.{nameof(GetUserProfile)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpGet("nearby/{radiusInKm}")]
    [ProducesResponseType(typeof(OpResult<ProfileDataPublic>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<ProfileDataPublic>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<ProfileDataPublic>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult GetUsersNearby(int radiusInKm)
    {
        logger.LogInformation($"{nameof(ProfileController)}.{nameof(GetUserProfile)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<List<ProfileDataPublic>> opResult = profileHandler.GetPeopleNearby(HttpContext, radiusInKm);
        logger.LogInformation($"{nameof(ProfileController)}.{nameof(GetUserProfile)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(OpResult<ProfileData>), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(OpResult<ProfileData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<ProfileData>), (int)HttpStatusCode.Conflict)]
    [ProducesResponseType(typeof(OpResult<ProfileData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult AddUserProfile([FromBody] ProfileDataCreate rawUserData)
    {
        logger.LogInformation($"{nameof(ProfileController)}.{nameof(AddUserProfile)} => Started.");
        OpResult<ProfileData> opResult = profileHandler.AddUserProfile(rawUserData);
        logger.LogInformation($"{nameof(ProfileController)}.{nameof(AddUserProfile)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = $"{nameof(Role.User)},{nameof(Role.InvitedUser)}")]
    [HttpPatch]
    [ProducesResponseType(typeof(OpResult<ProfileData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<ProfileData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<ProfileData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<ProfileData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult UpdateUserProfile([FromBody] ProfileDataUpdate userInfo)
    {
        logger.LogInformation($"{nameof(ProfileController)}.{nameof(UpdateUserProfile)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<ProfileData> opResult = profileHandler.UpdateUserProfile(HttpContext, userInfo);
        logger.LogInformation($"{nameof(ProfileController)}.{nameof(UpdateUserProfile)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = $"{nameof(Role.User)}")]
    [HttpPatch("geo/{latitude}/{longitude}")]
    [ProducesResponseType(typeof(OpResult<ProfileData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<ProfileData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<ProfileData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<ProfileData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult UpdateUserProfileGeo(double latitude, double longitude)
    {
        logger.LogInformation($"{nameof(ProfileController)}.{nameof(UpdateUserProfile)} => Started by User:  {ContextHelper.GetLoggedInUser(HttpContext)?.Id} .");
        OpResult<ProfileData> opResult = profileHandler.UpdateGeoCoordinates(HttpContext, latitude, longitude);
        logger.LogInformation($"{nameof(ProfileController)}.{nameof(UpdateUserProfile)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }


    [Authorize(Roles = $"{nameof(Role.User)},{nameof(Role.InvitedUser)}")]
    [HttpPost("photo")]
    [ProducesResponseType(typeof(OpResult<ProfileData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<ProfileData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<ProfileData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<ProfileData>), (int)HttpStatusCode.InternalServerError)]
    [Consumes("multipart/form-data")]
    public IActionResult UpdateUserProfileImage(IFormFile file)
    {
        logger.LogInformation($"{nameof(ProfileController)}.{nameof(UpdateUserProfileImage)} => Started by User:  {ContextHelper.GetLoggedInUser(HttpContext)?.Id} .");
        OpResult<bool> opResult = profileHandler.UpdateProfilePhoto(HttpContext, file);
        logger.LogInformation($"{nameof(ProfileController)}.{nameof(UpdateUserProfileImage)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }
}