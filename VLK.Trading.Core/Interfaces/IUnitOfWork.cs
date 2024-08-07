using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLK.Trading.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPortfolioRepository Portfolios { get; }
        IStockRepository Stocks { get; }
        IUserRepository Users { get; }
        void Commit();
    }
}
