﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFiV2.Models
{
    public class Stock
    {
        public string SymbolWithExchange { get; set; }

        public string? Name { get; set; }

        public string? Type { get; set; }

        public string? CountryName { get; set; }

        public string? CurrencyCode { get; set; }
        public string? CurrencyName { get; set; }

        public string? Isin { get; set; }
        public string? Description { get; set; }
        public string? Industry { get; set; }
        public string? Sector { get; set; }
        public string? LogoURL { get; set; }
    }
}
