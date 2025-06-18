using IFiV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFiV2.Shared.Services.Interfaces
{
    public interface IStockMarketService
    {
        Task<IReadOnlyList<StockPosition>> GetStockPositionsAsync();
    }
}
