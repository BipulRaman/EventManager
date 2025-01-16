using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Basic.Utilities;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EventManager.App.Api.Extended.Controllers;

[Route("business")]
[ApiController]
public class BusinessController : Controller
{
    private readonly IBusinessHandler businessHandler;
    private readonly ILogger<ProfileController> logger;

    public BusinessController(IBusinessHandler businessHandler, ILogger<ProfileController> logger)
    {
        this.businessHandler = businessHandler;
        this.logger = logger;
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpGet]
    [ProducesResponseType(typeof(OpResult<List<BusinessData>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<List<BusinessData>>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<List<BusinessData>>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult GetBusinesses()
    {
        logger.LogInformation($"{nameof(BusinessController)}.{nameof(GetBusinesses)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<List<BusinessData>> opResult = businessHandler.GetBusinesses(HttpContext);
        logger.LogInformation($"{nameof(BusinessController)}.{nameof(GetBusinesses)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpGet("nearby/{pinCode}")]
    [ProducesResponseType(typeof(OpResult<List<BusinessData>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<List<BusinessData>>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<List<BusinessData>>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult GetBusinesses(int pinCode)
    {
        logger.LogInformation($"{nameof(BusinessController)}.{nameof(GetBusinesses)} => Started by User:  {ContextHelper.GetLoggedInUser(HttpContext)?.Id} .");
        OpResult<List<BusinessData>> opResult = businessHandler.GetBusinesses(pinCode);
        logger.LogInformation($"{nameof(BusinessController)}.{nameof(GetBusinesses)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpGet("nearby/{pinCode}/{category}")]
    [ProducesResponseType(typeof(OpResult<List<BusinessData>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<List<BusinessData>>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<List<BusinessData>>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult GetBusinesses(int pinCode, string category)
    {
        logger.LogInformation($"{nameof(BusinessController)}.{nameof(GetBusinesses)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<List<BusinessData>> opResult = businessHandler.GetBusinesses(pinCode, category);
        logger.LogInformation($"{nameof(BusinessController)}.{nameof(GetBusinesses)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpGet("{businessId}")]
    [ProducesResponseType(typeof(OpResult<BusinessData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<BusinessData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<BusinessData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<BusinessData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult GetBusiness(string businessId)
    {
        logger.LogInformation($"{nameof(BusinessController)}.{nameof(GetBusiness)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<BusinessData> opResult = businessHandler.GetBusiness(HttpContext, businessId);
        logger.LogInformation($"{nameof(BusinessController)}.{nameof(GetBusiness)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpPost]
    [ProducesResponseType(typeof(OpResult<BusinessData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<BusinessData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<BusinessData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<BusinessData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult CreateBusiness([FromBody] BusinessDataCreate businessData)
    {
        logger.LogInformation($"{nameof(BusinessController)}.{nameof(CreateBusiness)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<BusinessData> opResult = businessHandler.CreateBusiness(HttpContext, businessData);
        logger.LogInformation($"{nameof(BusinessController)}.{nameof(CreateBusiness)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpPatch]
    [ProducesResponseType(typeof(OpResult<BusinessData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<BusinessData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<BusinessData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<BusinessData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult UpdateBusiness([FromBody] BusinessDataUpdate businessData)
    {
        logger.LogInformation($"{nameof(BusinessController)}.{nameof(UpdateBusiness)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<BusinessData> opResult = businessHandler.UpdateBusiness(HttpContext, businessData);
        logger.LogInformation($"{nameof(BusinessController)}.{nameof(UpdateBusiness)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.User))]
    [HttpDelete("{businessId}")]
    [ProducesResponseType(typeof(OpResult<BusinessData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<BusinessData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<BusinessData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<BusinessData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult DeleteBusiness(string businessId)
    {
        logger.LogInformation($"{nameof(BusinessController)}.{nameof(DeleteBusiness)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<BusinessData> opResult = businessHandler.DeleteBusiness(HttpContext, businessId);
        logger.LogInformation($"{nameof(BusinessController)}.{nameof(DeleteBusiness)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }
}
