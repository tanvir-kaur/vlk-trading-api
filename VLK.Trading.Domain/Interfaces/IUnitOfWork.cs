using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLK.Trading.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPortfolioRepository Portfolios { get; }
        IPortfolioStockRepository PortfolioStocks { get; }
        IStocksRepository Stocks { get; }
        IUserRepository Users { get; }
        void Commit();
    }
}
