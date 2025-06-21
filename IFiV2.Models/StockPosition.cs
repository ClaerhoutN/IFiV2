using System.Collections.Frozen;

namespace IFiV2.Models
{
    public class StockPosition
    {
        public required Stock Stock { get; init; }
        private IReadOnlyList<StockDataPoint> _historicalData;
        public required IReadOnlyList<StockDataPoint> HistoricalData
        {
            get => _historicalData;
            set
            {
                _historicalData = value;
                RecalculateProperties();
            }
        }
        public StockDataPoint LatestDataPoint { get; private set; }
        public float _24HChange { get; private set; }
        private void RecalculateProperties()
        {
            var sorted = _historicalData.OrderByDescending(x => x.Timestamp).ToList();

            LatestDataPoint = sorted.FirstOrDefault() ?? new StockDataPoint(Stock, new());

            var previousDataPoint = sorted.SkipWhile(sdp => sdp.Timestamp > LatestDataPoint.Timestamp.AddDays(-1)).FirstOrDefault();
            if(previousDataPoint == null)
                _24HChange = 0f;
            else
                _24HChange = (float)((LatestDataPoint.Close - previousDataPoint.Close) / previousDataPoint.Close);
        }
    }
}
