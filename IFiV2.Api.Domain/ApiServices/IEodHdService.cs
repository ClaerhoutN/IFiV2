using IFiV2.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFiV2.Api.Domain.ApiServices
{
    public interface IEodHdService
    {
        [Get("/eod/{symbol}?period=d&fmt=json")]
        Task<IReadOnlyList<IFiV2.Api.Domain.Dto.StockDataPoint>> GetEodAsync(string symbol, DateOnly from, DateOnly to);
    }
}
