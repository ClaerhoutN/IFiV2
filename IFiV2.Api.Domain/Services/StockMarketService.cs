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
            List<StockDataPoint> stockDataPoints = new List<StockDataPoint>();
            foreach (var symbol in symbolsWithExchange)
            {
                var stockDataPointsFromApi = (await _eodHdService.GetEodAsync(symbol, DateOnly.FromDateTime(from.Date), DateOnly.FromDateTime(to.Date)))
                    .Select(x => new StockDataPoint
                    {
                        SymbolWithExchange = symbol,
                        Timestamp = x.Date,
                        Open = x.Open,
                        High = x.High,
                        Low = x.Low,
                        Close = x.Close,
                        Adjusted_close = x.Adjusted_close,
                        Volume = x.Volume
                    });
                stockDataPoints.AddRange(stockDataPointsFromApi);
            }
            return stockDataPoints;
        }

        public async Task<IReadOnlyList<Stock>> SearchAsync(string query)
        {
            var stocks = await _eodHdService.SearchAsync(query);
            return stocks.Select(stock => new Stock
            {
                SymbolWithExchange = $"{stock.Code}.{stock.Exchange}",
                Name = stock.Name,
                Currency = stock.Currency,
                Type = stock.Type, 
                Country = stock.Country, 
                Isin = stock.Isin
            }).ToList();
        }
    }
}
