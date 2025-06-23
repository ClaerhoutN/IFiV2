using System.Collections.Frozen;

namespace IFiV2.Models
{
    public class StockPosition
    {
        public required Stock Stock { get; init; }
        private IReadOnlyList<StockDataPoint> _historicalData;
        public IReadOnlyList<StockDataPoint> DayDatapoints { get; private set; }
        public required IReadOnlyList<StockDataPoint> HistoricalData
        {
            get => _historicalData;
            set
            {
                _historicalData = value.OrderByDescending(x => x.Timestamp).ToList(); //todo: is ordering really necessary?
                RecalculateProperties();
            }
        }
        public float _1DChange { get; private set; }
        public float _7DChange { get; private set; }
        public float _30DChange { get; private set; }
        public float _90DChange { get; private set; }
        public float _1YChange { get; private set; }
        private void RecalculateProperties()
        {
            _1DChange = CalculateChange(1, Interval._1d);
            DayDatapoints = GetDataPointsForPeriod(1, Interval._5m);
            if(DayDatapoints.Count == 0)
                DayDatapoints = GetDataPointsForPeriod(1, Interval._1m);
            if (DayDatapoints.Count == 0)
                DayDatapoints = GetDataPointsForPeriod(1, Interval._1h);
            _7DChange = CalculateChange(7, Interval._1d);
            _30DChange = CalculateChange(30, Interval._1d);
            _90DChange = CalculateChange(90, Interval._1d);
            _1YChange = CalculateChange(365, Interval._1d);
        }

        private float CalculateChange(int days, Interval interval)
        {
            var latestDataPoint = _historicalData
                .Where(x => x.Interval == interval)
                .FirstOrDefault();
            if(latestDataPoint == null)
                return 0f;
            var previousDataPoint = _historicalData
                .Where(x => x.Interval == interval)
                .SkipWhile(sdp => sdp.Timestamp > latestDataPoint.Timestamp.AddDays(-days))
                .FirstOrDefault();
            if (previousDataPoint == null || previousDataPoint.Close == 0)
                return 0f;
            return (float)((latestDataPoint.Close - previousDataPoint.Close) / previousDataPoint.Close);
        }

        IReadOnlyList<StockDataPoint> GetDataPointsForPeriod(int days, Interval interval)
        {
            var latestDataPoint = _historicalData
                .Where(x => x.Interval == interval)
                .FirstOrDefault();
            if (latestDataPoint == null)
                return [];
            return _historicalData
                .Where(x => x.Interval == interval)
                .TakeWhile(x => x.Timestamp > latestDataPoint.Timestamp.AddDays(-1)).Reverse().ToList();
        }
    }
}
