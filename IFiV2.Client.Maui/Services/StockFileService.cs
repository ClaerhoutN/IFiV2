using IFiV2.Client.Shared.Services.Interfaces;
using IFiV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFiV2.Client.Maui.Services
{
    internal class StockFileService : IStockFileService
    {
        private const int _version = 1;
        private readonly string _stockFilePath = Path.Combine(FileSystem.Current.CacheDirectory + "stocks.json");
        private readonly string _stockDataPointFilePath = Path.Combine(FileSystem.Current.CacheDirectory + "stockDataPoints.json");
        public async Task SaveAsync(IEnumerable<StockPosition> stockPositions)
        {
            await File.WriteAllTextAsync(_stockFilePath, $"v{_version} " + System.Text.Json.JsonSerializer.Serialize(stockPositions.Select(x => x.Stock)));
            await File.WriteAllTextAsync(_stockDataPointFilePath, $"v{_version} " + System.Text.Json.JsonSerializer.Serialize(
                stockPositions.ToDictionary(x => x.Stock.SymbolWithExchange, x => x.HistoricalData) //general stock data is not serialized
                ));
            await Task.CompletedTask;
        }

        public async Task<List<StockPosition>> ReadAsync()
        {
            List<Stock> stocks;
            Dictionary<string, List<StockDataPoint>> stockDataPoints;

            using var stockStream = File.Open(_stockFilePath, FileMode.OpenOrCreate, FileAccess.Read);
            if (!IsStreamValidVersion(stockStream))
                return new List<StockPosition>();
            else
                stocks = System.Text.Json.JsonSerializer.Deserialize<List<Stock>>(stockStream);

            using var stockDataPointsStream = File.Open(_stockDataPointFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (!IsStreamValidVersion(stockDataPointsStream))
                stockDataPoints = new Dictionary<string, List<StockDataPoint>>();
            else
                stockDataPoints = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, List<StockDataPoint>>>(stockDataPointsStream);
            
            return stocks.Select(stock =>
            {
                if (!stockDataPoints.TryGetValue(stock.SymbolWithExchange, out var dataPoints)) //data point was not saved to the file, or the file was just created
                {
                    return new StockPosition
                    {
                        Stock = stock,
                        HistoricalData = new List<StockDataPoint>()
                    };
                }
                return new StockPosition
                {
                    Stock = stock,
                    HistoricalData = dataPoints.Select(x => new StockDataPoint(stock, x)).ToList()
                };
            }).ToList();
        }

        private static bool IsStreamValidVersion(FileStream fileStream)
        {
            StringBuilder version = new StringBuilder();
            int iB; char c = (char)0x0;
            while ((iB = fileStream.ReadByte()) != -1)
            {
                c = (char)(byte)iB;
                if (c == ' ')
                    break;
                version.Append(c);
            }
            if (version.ToString() != $"v{_version}")
                return false;
            return true;
        }
    }
}
