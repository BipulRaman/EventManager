using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Extended.Models;

namespace EventManager.App.Api.Extended.Interfaces;

public interface IExpenseHandler
{
    /// <summary>
    /// Get all Expenses.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <returns></returns>
    OpResult<List<ExpenseData>> GetExpenses(HttpContext httpContext);

    /// <summary>
    /// Get Expense by id.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="expenseId">Expense identity</param>
    /// <returns></returns>
    OpResult<ExpenseData> GetExpense(HttpContext httpContext, string expenseId);

    /// <summary>
    /// Create Expense.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="rawExpenseData">Raw Expense creation entity.</param>
    /// <returns></returns>
    OpResult<ExpenseData> CreateExpense(HttpContext httpContext, ExpenseData rawExpenseData);

    /// <summary>
    /// Update Expense.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="ExpenseData">Expense entity.</param>
    /// <returns></returns>
    OpResult<ExpenseData> UpdateExpense(HttpContext httpContext, ExpenseData ExpenseData);

    /// <summary>
    /// Delete Expense.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="ExpenseId">Expense entity.</param>
    /// <returns></returns>
    OpResult<bool> DeleteExpense(HttpContext httpContext, string ExpenseId);
}
