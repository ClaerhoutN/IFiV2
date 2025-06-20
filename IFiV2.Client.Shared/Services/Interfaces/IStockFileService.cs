﻿using IFiV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFiV2.Client.Shared.Services.Interfaces
{
    public interface IStockFileService
    {
        Task SaveAsync(IEnumerable<StockPosition> stockPositions);
        Task<List<StockPosition>> ReadAsync();
    }
}
