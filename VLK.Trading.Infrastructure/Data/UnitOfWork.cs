using VLK.Trading.Domain.Interfaces;

namespace VLK.Trading.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TradingDBContext _dbContext;
        public IStocksRepository Stocks { get; }
        public IPortfolioRepository Portfolios { get; }
        public IPortfolioStockRepository PortfolioStocks { get; }
        public IUserRepository Users { get; }

        public UnitOfWork(TradingDBContext dbContext,
                            IPortfolioRepository portfolios,
                            IStocksRepository stocks,
                            IPortfolioStockRepository portfolioStocks,
                            IUserRepository users)
        {
            _dbContext = dbContext;
            Portfolios = portfolios;
            Stocks = stocks;
            PortfolioStocks = portfolioStocks;
            Users = users;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

    }
}
