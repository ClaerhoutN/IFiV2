using IFiV2.Client.Shared.Services.Interfaces;
using IFiV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace IFiV2.Client.Shared.Services
{
    public class StockMarketService(Shared.ApiServices.IStockMarketService _stockMarketService, IStockFileService _stockFileService) : IStockMarketService
    {
        private static List<StockPosition> _stockPositions = new List<StockPosition>();
        public async Task AddStockPositionAsync(Stock stock)
        {
            _stockPositions.Add(await CreateStockPositionAsync(stock));
            await _stockFileService.SaveAsync(_stockPositions);
        }

        public async Task RemoveStockPositiomAsync(StockPosition stockPosition)
        {
            _stockPositions.Remove(stockPosition);
            await _stockFileService.SaveAsync(_stockPositions);
        }

        public async Task<IReadOnlyList<StockPosition>> GetStockPositionsAsync(bool fromFile = false, bool refreshDataPoints = false)
        {
            if (fromFile)
                _stockPositions = await _stockFileService.ReadAsync();
            if (refreshDataPoints)
            {
                var dataPointsDictionary = await GetCompletedStockDataPointsAsync(_stockPositions.Select(x => (x.Stock.SymbolWithExchange, x.HistoricalData)).ToArray());
                foreach (var stockPosition in _stockPositions)
                {
                    stockPosition.HistoricalData = dataPointsDictionary[stockPosition.Stock.SymbolWithExchange]
                        .Select(sdp => new StockDataPoint(stockPosition.Stock, sdp)).ToList();
                }
                await _stockFileService.SaveAsync(_stockPositions);
            }
            return _stockPositions;
        }

        private async Task<StockPosition> CreateStockPositionAsync(Stock stock)
        {      
            var stockDataPoints = await GetStockDataPoints(stock.SymbolWithExchange);
            return new StockPosition
            {
                Stock = stock,
                HistoricalData = stockDataPoints.Select(sdp => new StockDataPoint(stock, sdp)).ToList()
            };
        }

        public Task<IReadOnlyList<Stock>> SearchAsync(string search)
        {
            return _stockMarketService.SearchAsync(search);
        }

        public StockPosition GetStockPosition(string symbolWithExchange)
        {
            return _stockPositions.FirstOrDefault(sp => sp.Stock.SymbolWithExchange == symbolWithExchange);
        }
        private async Task<IReadOnlyList<StockDataPoint>> GetStockDataPoints(string symbolWithExchange) => (await GetCompletedStockDataPointsAsync((symbolWithExchange, null))).First().Value;
        private async Task<Dictionary<string, List<StockDataPoint>>> GetCompletedStockDataPointsAsync(params (string, IReadOnlyList<StockDataPoint>)[] dataPoints)
        {
            Dictionary<string, List<StockDataPoint>> dataPointsDictionary = new Dictionary<string, List<StockDataPoint>>();
            (Interval, DateTimeOffset, DateTimeOffset)[] intervals = [
                (Interval._1d, DateTimeOffset.Now.AddDays(-390), DateTimeOffset.Now), //added 10 addition days to ensure we have enough data for 1 year change calculation (e.g. the last close was a few days ago due to holidays)
                (Interval._1h, DateTimeOffset.Now.AddDays(-7), DateTimeOffset.Now),
                (Interval._5m, DateTimeOffset.Now.AddDays(-1), DateTimeOffset.Now)
                ];
            foreach(var (interval, from, to) in intervals)
            {
                var symbolsWithExchange = dataPoints.Select(dp => dp.Item1).ToArray();
                var newDataPoints = await _stockMarketService.GetStockDataPointsAsync(symbolsWithExchange, interval, from, to);
                foreach(var dataPointGroup in newDataPoints.GroupBy(dp => dp.SymbolWithExchange))
                {
                    if (!dataPointsDictionary.ContainsKey(dataPointGroup.Key))
                        dataPointsDictionary[dataPointGroup.Key] = new List<StockDataPoint>();
                    dataPointsDictionary[dataPointGroup.Key].AddRange(dataPointGroup);
                }
            }
            return dataPointsDictionary;
        }
    }
}
