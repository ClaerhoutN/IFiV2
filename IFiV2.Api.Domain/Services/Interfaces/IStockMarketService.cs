using IFiV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFiV2.Api.Domain.Services.Interfaces
{
    public interface IStockMarketService
    {
        Task<IReadOnlyList<StockDataPoint>> GetStockDataPointsAsync(string[] symbolsWithExchange, Interval interval, DateTimeOffset from, DateTimeOffset to);
        Task<IReadOnlyList<Stock>> SearchAsync(string query);
    }
}
