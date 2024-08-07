using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLK.Trading.Application.DTOModels;
using VLK.Trading.Domain.Models;

namespace VLK.Trading.Application.Interfaces
{
    public interface IStocksService
    {
        Task<List<StockDTO>> GetAllStocksAsync();
        Task<StockDTO> GetStockDetailsByTickerSymbolAsync(string tickerSymbol);
        Task BuyStockAsync(Guid userId, int buyingStockUnit, string stockTicker);
    }
}
