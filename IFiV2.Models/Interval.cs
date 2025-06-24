using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFiV2.Models
{
    public enum Interval
    {
        _1m, 
        _5m,
        _15m,
        _1h, 
        _1d
    }

    public static class IntervalExtensions
    {
        public static TimeSpan ToTimeSpan(this Interval interval)
        {
            return interval switch
            {
                Interval._1m => TimeSpan.FromMinutes(1),
                Interval._5m => TimeSpan.FromMinutes(5),
                Interval._15m => TimeSpan.FromMinutes(15),
                Interval._1h => TimeSpan.FromHours(1),
                Interval._1d => TimeSpan.FromDays(1),
                _ => throw new ArgumentOutOfRangeException(nameof(interval), interval, null)
            };
        }
    }
}
