using Microsoft.EntityFrameworkCore;
using VLK.Trading.Domain.Interfaces;
using VLK.Trading.Domain.Models;
using VLK.Trading.Infrastructure.Data;


namespace VLK.Trading.Infrastructure.Repositories
{
    public class StocksRepository : IStocksRepository
    {
        public readonly TradingDBContext _dbContext;
        public StocksRepository(TradingDBContext context)
        {
            _dbContext = context;
        }

        public async Task<List<Stock>> GetAllStocks()
        {
            return await _dbContext.Set<Stock>().Include(x => x.StockPrice).ToListAsync();
        }

        public async Task<Stock> GetStockDetailsTickerSymbol(string tickerSymbol)
        {
            return await _dbContext.Set<Stock>().Include(x => x.StockPrice).FirstOrDefaultAsync(stock => stock.TickerSymbol.Contains(tickerSymbol));
        }
    }
}
