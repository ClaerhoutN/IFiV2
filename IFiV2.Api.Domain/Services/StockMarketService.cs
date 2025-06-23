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
                if (interval >= Interval._1d) //eod
                {
                    var stockDataPointsFromApi = (await _eodHdService.GetEodAsync(symbol, DateOnly.FromDateTime(from.Date), DateOnly.FromDateTime(to.Date)))
                        .Select(x => new StockDataPoint
                        {
                            SymbolWithExchange = symbol,
                            Timestamp = new DateTimeOffset(x.UtcDate, new TimeSpan(0)),
                            Open = x.Open,
                            High = x.High,
                            Low = x.Low,
                            Close = x.Close,
                            Adjusted_close = x.Adjusted_close,
                            Volume = x.Volume
                        });
                    stockDataPoints.AddRange(stockDataPointsFromApi);
                }
                else //intraday
                {
                    if (interval == Interval._15m) //only 1m, 5m and 1h are available in eodhd intraday API
                        interval = Interval._5m;
                    var stockDataPointsFromApi = (await _eodHdService.GetIntradayAsync(symbol, interval.ToString().Substring(1), from.ToUnixTimeSeconds(), to.ToUnixTimeSeconds()))
                        
                        .Select(x => new StockDataPoint
                        {
                            SymbolWithExchange = symbol,
                            Timestamp = new DateTimeOffset(x.UtcDate, new TimeSpan(0)),
                            Open = x.Open,
                            High = x.High,
                            Low = x.Low,
                            Close = x.Close,
                            Adjusted_close = x.Adjusted_close,
                            Volume = x.Volume
                        });
                    stockDataPoints.AddRange(stockDataPointsFromApi);
                }
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
