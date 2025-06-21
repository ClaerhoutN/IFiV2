using System.Collections.Frozen;

namespace IFiV2.Models
{
    public class StockPosition
    {
        public Stock Stock => HistoricalData.FirstOrDefault();
        public IReadOnlyList<StockDataPoint> HistoricalData { get; init; } = new List<StockDataPoint>();
    }
}
