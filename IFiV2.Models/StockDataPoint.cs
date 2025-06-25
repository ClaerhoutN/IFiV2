using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IFiV2.Models
{
    public class StockDataPoint : Stock
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

        #region override properties from base class to prevent them from being serialized
        //[JsonIgnore]
        public override string SymbolWithExchange
        {
            get => _stock?.SymbolWithExchange ?? base.SymbolWithExchange;
            set { if (_stock != null) _stock.SymbolWithExchange = value; else base.SymbolWithExchange = value; }
        }
        [JsonIgnore]
        public override string Name
        {
            get => _stock?.Name ?? base.Name;
            set { if (_stock != null) _stock.Name = value; else base.Name = value; }
        }
        [JsonIgnore]
        public override string Type
        {
            get => _stock?.Type ?? base.Type;
            set { if (_stock != null) _stock.Type = value; else base.Type = value; }
        }
        [JsonIgnore]
        public override string Country
        {
            get => _stock?.Country ?? base.Country;
            set { if (_stock != null) _stock.Country = value; else base.Country = value; }
        }
        [JsonIgnore]
        public override string Currency
        {
            get => _stock?.Currency ?? base.Currency;
            set { if (_stock != null) _stock.Currency = value; else base.Currency = value; }
        }
        [JsonIgnore]
        public override string Isin
        {
            get => _stock?.Isin ?? base.Isin;
            set { if (_stock != null) _stock.Isin = value; else base.Isin = value; }
        }
        #endregion override properties from base class to prevent them from being serialized
    }
}
