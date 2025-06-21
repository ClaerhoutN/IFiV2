using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFiV2.Models
{
    public class StockDataPoint : Stock
    {
        public StockDataPoint()
        {            
        }
        public StockDataPoint(Stock stock)
        {
            SymbolWithExchange = stock.SymbolWithExchange;
            Name = stock.Name;
            Currency = stock.Currency;
            Type = stock.Type;
            Country = stock.Country;
            Isin = stock.Isin;
        }
        public DateTimeOffset Timestamp { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal Adjusted_close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public double Volume { get; set; }
    }
}
