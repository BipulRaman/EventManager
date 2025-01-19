using EventManager.App.Api.Extended.Models;

namespace EventManager.App.Api.Extended.Interfaces;

public interface IExpenseRepository
{
    /// <summary>
    /// Get Expense by id.
    /// </summary>
    /// <param name="rowKey">Expense identity.</param>
    /// <returns></returns>
    ExpenseEntity GetExpense(string rowKey);

    /// <summary>
    /// Get all Expenses.
    /// </summary>
    /// <returns></returns>
    List<ExpenseEntity> GetExpenses();

    /// <summary>
    /// Create Expense.
    /// </summary>
    /// <param name="expenseEntity">Expense entity.</param>
    /// <returns></returns>
    bool CreateExpense(ExpenseEntity expenseEntity);

    /// <summary>
    /// Update Expense.
    /// </summary>
    /// <param name="expenseEntity">Expense entity.</param>
    /// <returns></returns>
    bool UpdateExpense(ExpenseEntity expenseEntity);

    /// <summary>
    /// Delete Expense.
    /// </summary>
    /// <param name="partitionKey">Expense partition key.</param>
    /// <param name="rowKey">Expense row key.</param>
    /// <returns></returns>
    bool DeleteExpense(string partitionKey, string rowKey);
}
