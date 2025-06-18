using IFiV2.Models;
using IFiV2.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFiV2.Shared.Services
{
    public class StockMarketService(Shared.ApiServices.IStockMarketService _stockMarketService) : IStockMarketService
    {
        public async Task<IReadOnlyList<StockPosition>> GetStockPositionsAsync()
        {
            var stockDataPoints = await _stockMarketService.GetStockDataPointsAsync(["MCD.US"], Interval._1d, DateTimeOffset.Now.AddDays(-1), DateTimeOffset.Now);
            return new List<StockPosition>()
            {
                new StockPosition(),
                new StockPosition(),
                new StockPosition()
            };
        }
    }
}
