using IFiV2.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFiV2.Client.Shared.ApiServices
{
    public interface IStockMarketService
    {
        //[Get("/stocks")]
        //Task<IReadOnlyList<Stock>> GetStocks([Query(CollectionFormat.Multi)] string[] symbols);
        [Get("/stock-data-point")]
        Task<IReadOnlyList<StockDataPoint>> GetStockDataPointsAsync([Query(collectionFormat: CollectionFormat.Multi)] string[] symbolsWithExchange, Interval interval, DateTimeOffset from, DateTimeOffset to);
        [Get("/search")]
        Task<IReadOnlyList<Stock>> SearchAsync(string search);
        [Get("/fundamentals")]
        Task<Stock> GetFundamentalsAsync(string symbolWithExchange);
    }
}
