using System.Collections.Frozen;

namespace IFiV2.Models
{
    public class StockPosition
    {
        public required Stock Stock { get; init; }
        public required IReadOnlyList<StockDataPoint> HistoricalData { get; set; }
    }
}
