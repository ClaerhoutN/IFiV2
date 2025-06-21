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
    public class StockMarketService(Shared.ApiServices.IStockMarketService _stockMarketService) : IStockMarketService
    {
        private static List<StockPosition> _stockPositions = new List<StockPosition>();
        public async Task AddStockPositionAsync(Stock stock)
        {
            _stockPositions.Add(await GetStockPositionAsync(stock));
        }

        public async Task RemoveStockPositiomAsync(StockPosition stockPosition)
        {
            _stockPositions.Remove(stockPosition);
        }

        public async Task<IReadOnlyList<StockPosition>> GetStockPositionsAsync()
        {
            //var stockDataPoints = await _stockMarketService.GetStockDataPointsAsync(symbolsWithExchange, Interval._1d, DateTimeOffset.Now.AddDays(-1), DateTimeOffset.Now);
            //_stockPositions = stockDataPoints.GroupBy(x => x.SymbolWithExchange).Select(stockGroup =>
            //    new StockPosition 
            //    { 
            //        HistoricalData = stockGroup.ToList() 
            //    }).ToList();
            return _stockPositions;
        }

        private async Task<StockPosition> GetStockPositionAsync(Stock stock)
        {
            string[] symbolsWithExchange = [stock.SymbolWithExchange];
            var stockDataPoints = await _stockMarketService.GetStockDataPointsAsync(symbolsWithExchange, Interval._1d, DateTimeOffset.Now.AddDays(-1), DateTimeOffset.Now);
            
            return new StockPosition
            {
                HistoricalData = stockDataPoints.Select(sdp => Map(stock, sdp)).ToList()
            };
        }

        public Task<IReadOnlyList<Stock>> SearchAsync(string search)
        {
            return _stockMarketService.SearchAsync(search);
        }

        private static StockDataPoint Map(Stock stock, StockDataPoint stockDataPoint)
        {
            return new StockDataPoint(stock)
            {
                Timestamp = stockDataPoint.Timestamp,
                Open = stockDataPoint.Open,
                High = stockDataPoint.High,
                Low = stockDataPoint.Low,
                Close = stockDataPoint.Close,
                Adjusted_close = stockDataPoint.Adjusted_close,
                Volume = stockDataPoint.Volume,
            };
        }

        public StockPosition GetStockPosition(string symbolWithExchange)
        {
            return _stockPositions.FirstOrDefault(sp => sp.Stock.SymbolWithExchange == symbolWithExchange);
        }
    }
}
