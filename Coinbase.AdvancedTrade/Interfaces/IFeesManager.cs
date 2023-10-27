using System;
using System.Threading.Tasks;  
using Coinbase.AdvancedTrade.Models;

namespace Coinbase.AdvancedTrade.Interfaces
{
    /// <summary>
    /// Represents the manager for fee-related operations on the Coinbase platform.
    /// </summary>
    public interface IFeesManager
    {
        /// <summary>
        /// Asynchronously retrieves a summary of transactions within a specified date range.
        /// </summary>
        /// <param name="startDate">The start date of the transactions to retrieve.</param>
        /// <param name="endDate">The end date of the transactions to retrieve.</param>
        /// <param name="userNativeCurrency">The native currency of the user. Defaults to "USD".</param>
        /// <param name="productType">The type of product. Defaults to "SPOT".</param>
        /// <returns>A task representing the operation. The result of the task is a summary of the transactions or null if none are found.</returns>
        Task<TransactionsSummary?> GetTransactionsSummaryAsync(
            DateTime startDate,
            DateTime endDate,
            string userNativeCurrency = "USD",
            string productType = "SPOT"
        );
    }
}
