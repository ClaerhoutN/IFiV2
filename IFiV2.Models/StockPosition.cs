using System.Collections.Frozen;

namespace IFiV2.Models
{
    public class StockPosition
    {
        public Stock Stock => HistoricalData.FirstOrDefault() ?? new Stock(); //TODO: remove new()
        public IReadOnlyList<StockDataPoint> HistoricalData { get; set; } = new List<StockDataPoint>();
    }
}
