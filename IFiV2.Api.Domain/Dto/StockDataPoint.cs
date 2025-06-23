using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IFiV2.Api.Domain.Dto
{
    public class StockDataPoint
    {
        public long? Timestamp { get; set; }
        private DateTime _utcDate;
        [JsonPropertyName("date")]
        public DateTime UtcDate //because _date is not correctly deserialized, but we always have a timestamp in Unix time
        {
            get
            {
                if (_utcDate == DateTime.MinValue)
                {
                    if(Timestamp.HasValue)
                        _utcDate = DateTimeOffset.FromUnixTimeSeconds(Timestamp.Value).DateTime;
                    else
                        _utcDate = DateTime.UtcNow; //fallback if no timestamp is available
                }
                return _utcDate;
            }
            set => _utcDate = value;
        }
        //public int Gmtoffset { get; set; } //sometimes used for intraday
        public decimal? Open { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public decimal? Close { get; set; }
        public decimal? Adjusted_close { get; set; }
        public long? Volume { get; set; }
    }
}
