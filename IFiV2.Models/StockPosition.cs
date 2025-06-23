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
                _historicalData = value.OrderByDescending(x => x.Timestamp).ToList();
                RecalculateProperties();
            }
        }
        public StockDataPoint LatestDataPoint { get; private set; }
        public float _1DChange { get; private set; }
        public float _7DChange { get; private set; }
        public float _30DChange { get; private set; }
        public float _90DChange { get; private set; }
        public float _1YChange { get; private set; }
        private void RecalculateProperties()
        {
            LatestDataPoint = _historicalData.FirstOrDefault() ?? new StockDataPoint(Stock, new());
            _1DChange = CalculateChange(1);
            _7DChange = CalculateChange(7);
            _30DChange = CalculateChange(30);
            _90DChange = CalculateChange(90);
            _1YChange = CalculateChange(365);
        }

        private float CalculateChange(int days)
        {
            var previousDataPoint = _historicalData
                .SkipWhile(sdp => sdp.Timestamp > LatestDataPoint.Timestamp.AddDays(-days))
                .FirstOrDefault();
            if (previousDataPoint == null || previousDataPoint.Close == 0)
                return 0f;
            return (float)((LatestDataPoint.Close - previousDataPoint.Close) / previousDataPoint.Close);
        }
    }
}
