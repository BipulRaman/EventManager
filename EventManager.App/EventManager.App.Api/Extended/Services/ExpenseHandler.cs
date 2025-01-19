using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Basic.Utilities;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using System.Net;

namespace EventManager.App.Api.Extended.Services;

public class ExpensesHandler : IExpenseHandler
{
    private readonly IExpensesRepository expenseRepository;
    private readonly ILogger<ExpensesHandler> logger;

    public ExpensesHandler(IExpensesRepository expenseRepository, ILogger<ExpensesHandler> logger)
    {
        this.expenseRepository = expenseRepository;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public OpResult<List<ExpenseData>> GetExpenses(HttpContext httpContext)
    {
        logger.LogInformation($"{nameof(ExpensesHandler)}.{nameof(GetExpenses)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<List<ExpenseData>> opResult = new OpResult<List<ExpenseData>>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            List<ExpenseEntity> expenseEntities = expenseRepository.GetExpenses();
            List<ExpenseData> expenses = ConvertCollection(expenseEntities);
            opResult.Result = expenses;
            opResult.Status = HttpStatusCode.OK;
            opResult.ErrorCode = ErrorCode.None;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(ExpensesHandler)}.{nameof(GetExpenses)} => Error occurred while fetching Expenses for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }

        logger.LogInformation($"{nameof(ExpensesHandler)}.{nameof(GetExpenses)} => Method completed for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<ExpenseData> GetExpense(HttpContext httpContext, string expenseId)
    {
        logger.LogInformation($"{nameof(ExpensesHandler)}.{nameof(GetExpense)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<ExpenseData> opResult = new OpResult<ExpenseData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (!string.IsNullOrEmpty(expenseId))
            {
                ExpenseData expenseData = (ExpenseData)expenseRepository.GetExpense(expenseId);
                opResult.Result = expenseData;
                opResult.Status = HttpStatusCode.OK;
                opResult.ErrorCode = ErrorCode.None;
            }
            else
            {
                opResult.Status = HttpStatusCode.BadRequest;
                opResult.ErrorCode = ErrorCode.Common_BadRequest;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(ExpensesHandler)}.{nameof(GetExpense)} => Error occurred while fetching Expense for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }

        logger.LogInformation($"{nameof(ExpensesHandler)}.{nameof(GetExpense)} => Method completed for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<ExpenseData> CreateExpense(HttpContext httpContext, ExpenseData rawExpenseData)
    {
        logger.LogInformation($"{nameof(ExpensesHandler)}.{nameof(CreateExpense)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<ExpenseData> opResult = new OpResult<ExpenseData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (rawExpenseData is not null && rawExpenseData.IsValidToCreate() && httpContext is not null)
            {
                ExpenseEntity expenseEntity = rawExpenseData.ConvertToCreateEntity(httpContext);
                bool isSuccessful = expenseRepository.CreateExpense(expenseEntity);

                if (isSuccessful)
                {
                    opResult.Result = expenseEntity;
                    opResult.Status = HttpStatusCode.OK;
                    opResult.ErrorCode = ErrorCode.None;
                }
                else
                {
                    opResult.Status = HttpStatusCode.InternalServerError;
                    opResult.ErrorCode = ErrorCode.Entity_Create_Failed;
                }
            }
            else
            {
                opResult.Status = HttpStatusCode.BadRequest;
                opResult.ErrorCode = ErrorCode.Common_BadRequest;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(ExpensesHandler)}.{nameof(CreateExpense)} => Error occurred while creating Expense for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }

        logger.LogInformation($"{nameof(ExpensesHandler)}.{nameof(CreateExpense)} => Method completed for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<ExpenseData> UpdateExpense(HttpContext httpContext, ExpenseData expenseData)
    {
        logger.LogInformation($"{nameof(ExpensesHandler)}.{nameof(UpdateExpense)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<ExpenseData> opResult = new OpResult<ExpenseData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (expenseData is not null && expenseData.IsValidToUpdate() && httpContext is not null)
            {
                ExpenseEntity expenseEntity = expenseData.ConvertToUpdateEntity(httpContext);
                bool isSuccessful = expenseRepository.UpdateExpense(expenseEntity);

                if (isSuccessful)
                {
                    opResult.Result = expenseEntity;
                    opResult.Status = HttpStatusCode.OK;
                    opResult.ErrorCode = ErrorCode.None;
                }
                else
                {
                    opResult.Status = HttpStatusCode.InternalServerError;
                    opResult.ErrorCode = ErrorCode.Entity_Update_Failed;
                }
            }
            else
            {
                opResult.Status = HttpStatusCode.BadRequest;
                opResult.ErrorCode = ErrorCode.Common_BadRequest;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(ExpensesHandler)}.{nameof(UpdateExpense)} => Error occurred while updating Expense for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<bool> DeleteExpense(HttpContext httpContext, string expenseId)
    {
        logger.LogInformation($"{nameof(ExpensesHandler)}.{nameof(DeleteExpense)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<bool> opResult = new OpResult<bool>()
        {
            Result = false,
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (string.IsNullOrEmpty(expenseId) && httpContext is not null)
            {
                UserEntity contextUserInfo = (UserEntity)httpContext.Items[NameConstants.USER_KEY];
                bool isSuccessful = expenseRepository.DeleteExpense(contextUserInfo.RowKey, expenseId);

                if (isSuccessful)
                {
                    opResult.Result = true;
                    opResult.Status = HttpStatusCode.OK;
                    opResult.ErrorCode = ErrorCode.None;
                }
                else
                {
                    opResult.Status = HttpStatusCode.InternalServerError;
                    opResult.ErrorCode = ErrorCode.Entity_Delete_Failed;
                }
            }
            else
            {
                opResult.Status = HttpStatusCode.BadRequest;
                opResult.ErrorCode = ErrorCode.Common_BadRequest;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(ExpensesHandler)}.{nameof(DeleteExpense)} => Error occurred while deleting Expense for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }
        return opResult;
    }

    private List<ExpenseData> ConvertCollection(List<ExpenseEntity> expenseEntities)
    {
        List<ExpenseData> expenses = new List<ExpenseData>();

        if (expenseEntities is not null && expenseEntities.Count > 0)
        {
            foreach (ExpenseEntity expenseEntity in expenseEntities)
            {
                expenses.Add((ExpenseData)expenseEntity);
            }
        }

        return expenses;
    }
}
