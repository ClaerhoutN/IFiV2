﻿using IFiV2.Api.Domain.Dto;
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
        [Get("/intraday/{symbol}?period=d&fmt=json")]
        Task<IReadOnlyList<IFiV2.Api.Domain.Dto.StockDataPoint>> GetIntradayAsync(string symbol, string interval, [AliasAs("from")]long fromUnixUTC, [AliasAs("to")]long toUnixUTC);
        [Get("/search/{query}?fmt=json")]
        Task<IReadOnlyList<IFiV2.Api.Domain.Dto.Stock>> SearchAsync(string query);
        [Get("/fundamentals/{symbolWithExchange}?fmt=json")]
        Task<StockFundamentals> GetFundamentalsAsync(string symbolWithExchange);
    }
}
