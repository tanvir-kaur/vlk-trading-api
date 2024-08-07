using Microsoft.EntityFrameworkCore;
using VLK.Trading.Domain.Interfaces;
using VLK.Trading.Domain.Models;
using VLK.Trading.Infrastructure.Data;

namespace VLK.Trading.Infrastructure.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        public readonly TradingDBContext _dbContext;
        public PortfolioRepository(TradingDBContext context)
        {
            _dbContext = context;
        }

        public async Task<Portfolio> GetPortfolioByUserId(Guid userId)
        {
            return await _dbContext.Set<Portfolio>()
                                   .Include(x => x.User)
                                   .Include(x => x.PortfoliosStocks)
                                   .ThenInclude(x => x.Stock)
                                   .FirstOrDefaultAsync(x => x.UserId == userId);
        }

    }
}
