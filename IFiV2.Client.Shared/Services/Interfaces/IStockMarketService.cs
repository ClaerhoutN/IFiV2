using IFiV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFiV2.Client.Shared.Services.Interfaces
{
    public interface IStockMarketService
    {
        Task<IReadOnlyList<StockPosition>> GetStockPositionsAsync();
        Task AddStockPositionAsync(Stock stock);
        Task RemoveStockPositiomAsync(StockPosition stockPosition);
        Task<IReadOnlyList<Stock>> SearchAsync(string search);
        StockPosition GetStockPosition(string symbolWithExchange);
    }
}
