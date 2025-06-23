using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFiV2.Models
{
    public class Stock
    {
        private string _symbolWithExchange;
        public virtual string SymbolWithExchange
        {
            get => _symbolWithExchange;
            set => _symbolWithExchange = value;
        }

        private string _name;
        public virtual string Name
        {
            get => _name;
            set => _name = value;
        }

        private string _type;
        public virtual string Type
        {
            get => _type;
            set => _type = value;
        }

        private string _country;
        public virtual string Country
        {
            get => _country;
            set => _country = value;
        }

        private string _currency;
        public virtual string Currency
        {
            get => _currency;
            set => _currency = value;
        }

        private string? _isin;
        public virtual string? Isin
        {
            get => _isin;
            set => _isin = value;
        }
    }
}
