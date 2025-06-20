using IFiV2.Api.Domain.ApiServices;
using IFiV2.Api.Domain.Services.Interfaces;
using IFiV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFiV2.Api.Domain.Services
{
    public class StockMarketService(IEodHdService _eodHdService) : IStockMarketService
    {
        public async Task<IReadOnlyList<StockDataPoint>> GetStockDataPointsAsync(string[] symbolsWithExchange, Interval interval, DateTimeOffset from, DateTimeOffset to)
        {
            foreach(var symbol in symbolsWithExchange)
            {
                //var stockDataPoints = await _eodHdService.GetEodAsync(symbol, DateOnly.FromDateTime(from.Date), DateOnly.FromDateTime(to.Date));
            }
            return [new StockDataPoint { Symbol = "NYSE.AAPL" }];
        }
    }
}
