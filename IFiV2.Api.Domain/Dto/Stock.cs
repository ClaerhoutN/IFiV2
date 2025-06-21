using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFiV2.Api.Domain.Dto
{
    public class Stock
    {
        public string Code { get; set; }
        public string Exchange { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }        
        public string Isin { get; set; }
        public decimal PreviousClose { get; set; }
        public DateOnly PreviousCloseDate { get; set; }
    }
}
