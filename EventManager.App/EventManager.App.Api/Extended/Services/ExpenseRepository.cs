using Azure;
using Azure.Data.Tables;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using Microsoft.Extensions.Options;

namespace EventManager.App.Api.Extended.Services;

public class ExpenseRepository : IExpensesRepository
{
    private readonly TableClient tableClient;

    public ExpenseRepository(IOptions<AzureTableConfig> azureTableConfig)
    {
        TableServiceClient tableServiceClient = new TableServiceClient(azureTableConfig.Value.ConnectionString);
        tableClient = tableServiceClient.GetTableClient(azureTableConfig.Value.ExpensesTable);
    }

    /// <inheritdoc/>
    public ExpenseEntity GetExpense(string rowKey)
    {
        return tableClient.Query<ExpenseEntity>(e => e.RowKey.Equals(rowKey, StringComparison.Ordinal)).FirstOrDefault();
    }

    /// <inheritdoc/>
    public List<ExpenseEntity> GetExpenses()
    {
        return tableClient.Query<ExpenseEntity>().OrderByDescending(e => e.Timestamp).ToList();
    }

    /// <inheritdoc/>
    public bool CreateExpense(ExpenseEntity expenseEntity)
    {
        Response response = tableClient.AddEntity(expenseEntity);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }

    /// <inheritdoc/>
    public bool UpdateExpense(ExpenseEntity expenseEntity)
    {
        Response response = tableClient.UpdateEntity(expenseEntity, ETag.All, TableUpdateMode.Merge);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }

    /// <inheritdoc/>
    public bool DeleteExpense(string partitionKey, string rowKey)
    {
        Response response = tableClient.DeleteEntity(partitionKey, rowKey);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }
}
