using System.Threading.Tasks;
using Coinbase.AdvancedTrade.Models;

namespace Coinbase.AdvancedTrade.Interfaces
{
    public interface ICommonManager
    {
        /// <summary>
        /// Asynchronously retrieves the current server time from Coinbase.
        /// </summary>
        /// <returns>The server time details including ISO 8601 formatted date and time,
        /// number of seconds since Unix epoch, and number of milliseconds since Unix epoch,
        /// or null if the information is not available.</returns>
        Task<ServerTime?> GetCoinbaseServerTimeAsync();
    }
}
