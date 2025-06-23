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
            Dictionary<string, List<Dto.StockDataPoint>> stockDataPointDtos = new ();
            foreach (var symbol in symbolsWithExchange)
                stockDataPointDtos.Add(symbol, new List<Dto.StockDataPoint>());
            foreach (var symbol in symbolsWithExchange)
            {
                if (interval >= Interval._1d) //eod
                {
                    var stockDataPointsFromApi = await _eodHdService.GetEodAsync(symbol, DateOnly.FromDateTime(from.UtcDateTime), DateOnly.FromDateTime(to.UtcDateTime));
                    stockDataPointDtos[symbol].AddRange(stockDataPointsFromApi);
                }
                else //intraday
                {
                    if (interval == Interval._15m) //only 1m, 5m and 1h are available in eodhd intraday API
                        interval = Interval._5m;
                    var stockDataPointsFromApi = await _eodHdService.GetIntradayAsync(symbol, interval.ToString().Substring(1), from.ToUnixTimeSeconds(), to.ToUnixTimeSeconds());
                    stockDataPointDtos[symbol].AddRange(stockDataPointsFromApi);
                }
            }
            List<StockDataPoint> stockDataPoints = new List<StockDataPoint>(stockDataPointDtos.Sum(x => x.Value.Count));
            foreach (var symbol in symbolsWithExchange)
            {
                var orderedDataPoints = stockDataPointDtos[symbol].OrderByDescending(x => x.UtcDate).ToList();
                int i = 0;
                foreach (var dataPoint in orderedDataPoints)
                {
                    stockDataPoints.Add(new StockDataPoint
                    {
                        SymbolWithExchange = symbol,
                        Timestamp = new DateTimeOffset(dataPoint.UtcDate, new TimeSpan(0)),
                        //openeng price is not always available, so we use the last close price
                        Open = dataPoint.Open ?? FindLastAvailableValue(orderedDataPoints, i + 1, x => x.Close),
                        High = dataPoint.High ?? FindLastAvailableValue(orderedDataPoints, i + 1, x => x.Close),
                        Low = dataPoint.Low ?? FindLastAvailableValue(orderedDataPoints, i + 1, x => x.Close),
                        Close = dataPoint.Close ?? FindLastAvailableValue(orderedDataPoints, i + 1, x => x.Close),
                        Adjusted_close = dataPoint.Adjusted_close,
                        Volume = dataPoint.Volume ?? 0,
                    });
                    ++i;
                }
            }
            return stockDataPoints;
        }

        private static T FindLastAvailableValue<T>(List<Dto.StockDataPoint> stockDataPoints, int index, Func<Dto.StockDataPoint, T?> selector)
            where T : struct
        {
            for (int i = index; i < stockDataPoints.Count; ++i)
            {
                T? value = selector(stockDataPoints[i]);
                if (value.HasValue)
                    return value.Value;
            }
            return default;
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
