using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLK.Trading.Core.Models;

namespace VLK.Trading.Services.Interfaces
{
    public interface IStockService
    {
        Task<List<Stock>> GetAllStocksAsync();
        Task<Stock> GetStockDetailsByName(string stockName);
    }
}
