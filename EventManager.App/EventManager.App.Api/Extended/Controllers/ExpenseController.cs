using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Basic.Utilities;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EventManager.App.Api.Extended.Controllers;

[Route("expense")]
[ApiController]
public class ExpenseController : Controller
{
    private readonly IExpenseHandler ExpenseHandler;
    private readonly ILogger<ProfileController> logger;

    public ExpenseController(IExpenseHandler ExpenseHandler, ILogger<ProfileController> logger)
    {
        this.ExpenseHandler = ExpenseHandler;
        this.logger = logger;
    }

    [Authorize(Roles = nameof(Role.Expense))]
    [HttpGet]
    [ProducesResponseType(typeof(OpResult<List<ExpenseData>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<List<ExpenseData>>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<List<ExpenseData>>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult GetExpenses()
    {
        logger.LogInformation($"{nameof(ExpenseController)}.{nameof(GetExpenses)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<List<ExpenseData>> opResult = ExpenseHandler.GetExpenses(HttpContext);
        logger.LogInformation($"{nameof(ExpenseController)}.{nameof(GetExpenses)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.Expense))]
    [HttpGet("{ExpenseId}")]
    [ProducesResponseType(typeof(OpResult<ExpenseData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<ExpenseData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<ExpenseData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<ExpenseData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult GetExpense(string ExpenseId)
    {
        logger.LogInformation($"{nameof(ExpenseController)}.{nameof(GetExpense)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<ExpenseData> opResult = ExpenseHandler.GetExpense(HttpContext, ExpenseId);
        logger.LogInformation($"{nameof(ExpenseController)}.{nameof(GetExpense)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.Expense))]
    [HttpPost]
    [ProducesResponseType(typeof(OpResult<ExpenseData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<ExpenseData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<ExpenseData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<ExpenseData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult CreateExpense([FromBody] ExpenseDataCreate ExpenseEntity)
    {
        logger.LogInformation($"{nameof(ExpenseController)}.{nameof(CreateExpense)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<ExpenseData> opResult = ExpenseHandler.CreateExpense(HttpContext, ExpenseEntity);
        logger.LogInformation($"{nameof(ExpenseController)}.{nameof(CreateExpense)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.Admin))]
    [HttpPatch]
    [ProducesResponseType(typeof(OpResult<ExpenseData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<ExpenseData>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<ExpenseData>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<ExpenseData>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult UpdateExpense([FromBody] ExpenseDataUpdate ExpenseEntity)
    {
        logger.LogInformation($"{nameof(ExpenseController)}.{nameof(UpdateExpense)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<ExpenseData> opResult = ExpenseHandler.UpdateExpense(HttpContext, ExpenseEntity);
        logger.LogInformation($"{nameof(ExpenseController)}.{nameof(UpdateExpense)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }

    [Authorize(Roles = nameof(Role.Admin))]  
    [HttpDelete("{ExpenseId}")]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OpResult<bool>), (int)HttpStatusCode.InternalServerError)]
    public IActionResult DeleteExpense(string ExpenseId)
    {
        logger.LogInformation($"{nameof(ExpenseController)}.{nameof(DeleteExpense)} => Started by User: {ContextHelper.GetLoggedInUser(HttpContext)?.Id}.");
        OpResult<bool> opResult = ExpenseHandler.DeleteExpense(HttpContext, ExpenseId);
        logger.LogInformation($"{nameof(ExpenseController)}.{nameof(DeleteExpense)} => Completed. Response => Status: {opResult.Status}; ErrorCode: {opResult.ErrorCode}.");
        return StatusCode((int)opResult.Status, opResult);
    }
}
