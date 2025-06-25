using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IFiV2.Models
{
    public class StockDataPoint
    {
        private readonly Stock _stock;
        public StockDataPoint()
        {            
        }
        public StockDataPoint(Stock stock, StockDataPoint stockDataPoint)
        {
            _stock = stock;

            Interval = stockDataPoint.Interval;

            Timestamp = stockDataPoint.Timestamp;
            Open = stockDataPoint.Open;
            High = stockDataPoint.High;
            Low = stockDataPoint.Low;
            Close = stockDataPoint.Close;
            Adjusted_close = stockDataPoint.Adjusted_close;
            Volume = stockDataPoint.Volume;
        }
        public DateTimeOffset Timestamp { get; set; }
        public Interval Interval { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal? Adjusted_close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public double Volume { get; set; }
        private string _symbolWithExchange;
        public string SymbolWithExchange
        {
            get => _symbolWithExchange;
            set => _symbolWithExchange = value;
        }
    }
}
