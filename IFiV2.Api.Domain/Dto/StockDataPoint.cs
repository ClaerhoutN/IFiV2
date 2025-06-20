﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFiV2.Api.Domain.Dto
{
    public class StockDataPoint
    {
        public DateTimeOffset Date { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Adjusted_close { get; set; }
        public long Volume { get; set; }
    }
}
