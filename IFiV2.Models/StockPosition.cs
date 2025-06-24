using System.Collections.Frozen;

namespace IFiV2.Models
{
    public class StockPosition
    {
        public required Stock Stock { get; init; }
        private IReadOnlyList<StockDataPoint> _historicalData;
        public IReadOnlyList<StockDataPoint> DayDatapoints { get; private set; }
        public IReadOnlyList<StockDataPoint> WeekDatapoints { get; private set; }
        public IReadOnlyList<StockDataPoint> MonthDatapoints { get; private set; }
        public IReadOnlyList<StockDataPoint> QuarterDatapoints { get; private set; }
        public IReadOnlyList<StockDataPoint> YearDatapoints { get; private set; }
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
        public decimal _1DOpen;
        public decimal _1DClose;
        public decimal _1DHigh;
        public decimal _1DLow;
        public double _1DVolume;
        public float _7DChange { get; private set; }
        public decimal _7DOpen;
        public decimal _7DClose;
        public decimal _7DHigh;
        public decimal _7DLow;
        public double _7DVolume;
        public float _30DChange { get; private set; }
        public decimal _30DOpen;
        public decimal _30DClose;
        public decimal _30DHigh;
        public decimal _30DLow;
        public double _30DVolume;
        public float _90DChange { get; private set; }
        public decimal _90DOpen;
        public decimal _90DClose;
        public decimal _90DHigh;
        public decimal _90DLow;
        public double _90DVolume;
        public float _1YChange { get; private set; }
        public decimal _1YOpen;
        public decimal _1YClose;
        public decimal _1YHigh;
        public decimal _1YLow;
        public double _1YVolume;
        private void RecalculateProperties()
        {
            _1DChange = CalculateChange(1, Interval._1d);
            _7DChange = CalculateChange(7, Interval._1d);
            _30DChange = CalculateChange(30, Interval._1d);
            _90DChange = CalculateChange(90, Interval._1d);
            _1YChange = CalculateChange(365, Interval._1d);

            DayDatapoints = GetDataPointsForPeriod(1, Interval._5m, out _1DOpen, out _1DClose, out _1DHigh, out _1DLow, out _1DVolume);
            if (DayDatapoints.Count == 0)
                DayDatapoints = GetDataPointsForPeriod(1, Interval._1m, out _1DOpen, out _1DClose, out _1DHigh, out _1DLow, out _1DVolume);
            if (DayDatapoints.Count == 0)
                DayDatapoints = GetDataPointsForPeriod(1, Interval._1h, out _1DOpen, out _1DClose, out _1DHigh, out _1DLow, out _1DVolume);

            WeekDatapoints = GetDataPointsForPeriod(7, Interval._5m, out _7DOpen, out _7DClose, out _7DHigh, out _7DLow, out _7DVolume);
            if (WeekDatapoints.Count == 0)
                WeekDatapoints = GetDataPointsForPeriod(7, Interval._1m, out _7DOpen, out _7DClose, out _7DHigh, out _7DLow, out _7DVolume);
            if (WeekDatapoints.Count == 0)
                WeekDatapoints = GetDataPointsForPeriod(7, Interval._1h, out _7DOpen, out _7DClose, out _7DHigh, out _7DLow, out _7DVolume);

            MonthDatapoints = GetDataPointsForPeriod(30, Interval._1d, out _30DOpen, out _30DClose, out _30DHigh, out _30DLow, out _30DVolume);
            QuarterDatapoints = GetDataPointsForPeriod(90, Interval._1d, out _90DOpen, out _90DClose, out _90DHigh, out _90DLow, out _90DVolume);
            YearDatapoints = GetDataPointsForPeriod(365, Interval._1d, out _1YOpen, out _1YClose, out _1YHigh, out _1YLow, out _1YVolume);

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

        IReadOnlyList<StockDataPoint> GetDataPointsForPeriod(int days, Interval interval, out decimal open, out decimal close, out decimal high, out decimal low, out double volume)
        {
            open = close = high = low = 0M;
            volume = 0D;
            var latestDataPoint = _historicalData
                .Where(x => x.Interval == interval)
                .FirstOrDefault();
            if (latestDataPoint == null)
                return [];
            var dataPoints = _historicalData
                .Where(x => x.Interval == interval)
                .TakeWhile(x => x.Timestamp > latestDataPoint.Timestamp.AddDays(-days)).Reverse().ToList();

            open = dataPoints.First().Open;
            close = dataPoints.Last().Close;
            high = dataPoints.Max(dp => dp.High);
            low = dataPoints.Min(dp => dp.Low);
            volume = dataPoints.Sum(dp => dp.Volume);//todo: is this precise? better to take eod data points

            return dataPoints;
        }
    }
}
