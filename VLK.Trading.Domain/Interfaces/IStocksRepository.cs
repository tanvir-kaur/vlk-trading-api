using VLK.Trading.Domain.Models;

namespace VLK.Trading.Domain.Interfaces
{
    public interface IStocksRepository
    {
        Task<List<Stock>> GetAllStocks();
        Task<Stock> GetStockDetailsTickerSymbol(string tickerSymbol);
    }
}
