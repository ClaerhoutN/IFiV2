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
        public StockDataPoint()
        {            
        }
        public StockDataPoint(Stock stock, StockDataPoint stockDataPoint)
        {
            SymbolWithExchange = stock.SymbolWithExchange;
            Name = stock.Name;
            Currency = stock.Currency;
            Type = stock.Type;
            Country = stock.Country;
            Isin = stock.Isin;

            Timestamp = stockDataPoint.Timestamp;
            Open = stockDataPoint.Open;
            High = stockDataPoint.High;
            Low = stockDataPoint.Low;
            Close = stockDataPoint.Close;
            Adjusted_close = stockDataPoint.Adjusted_close;
            Volume = stockDataPoint.Volume;
        }
        public DateTimeOffset Timestamp { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal Adjusted_close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public double Volume { get; set; }

        #region override properties from base class to prevent them from being serialized
        [JsonIgnore]
        public override string SymbolWithExchange
        {
            get => base.SymbolWithExchange;
            set => base.SymbolWithExchange = value;
        }
        [JsonIgnore]
        public override string Name
        {
            get => base.Name;
            set => base.Name = value;
        }
        [JsonIgnore]
        public override string Type
        {
            get => base.Type;
            set => base.Type = value;
        }
        [JsonIgnore]
        public override string Country
        {
            get => base.Country;
            set => base.Country = value;
        }
        [JsonIgnore]
        public override string Currency
        {
            get => base.Currency;
            set => base.Currency = value;
        }
        [JsonIgnore]
        public override string Isin
        {
            get => base.Isin;
            set => base.Isin = value;
        }
        #endregion override properties from base class to prevent them from being serialized
    }
}
