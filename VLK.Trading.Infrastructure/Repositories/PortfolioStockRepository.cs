using Microsoft.EntityFrameworkCore;
using VLK.Trading.Domain.Interfaces;
using VLK.Trading.Domain.Models;
using VLK.Trading.Infrastructure.Data;

namespace VLK.Trading.Infrastructure.Repositories
{
    public class PortfolioStockRepository : IPortfolioStockRepository
    {
        public readonly TradingDBContext _dbContext;
        public PortfolioStockRepository(TradingDBContext context)
        {
            _dbContext = context;
        }

        public async Task<PortfolioStock> GetByPortfolioIdAndStockName(int portfolioId, string stockName)
        {
            return await _dbContext.Set<PortfolioStock>().Where(x => x.PortfolioId == portfolioId && x.Stock.Name == stockName).FirstOrDefaultAsync();
        }

        public async Task Insert(PortfolioStock portfolioStock)
        {
            await _dbContext.Set<PortfolioStock>().AddAsync(portfolioStock);
        }
    }
}
