using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLK.Trading.Domain.Models;

namespace VLK.Trading.Domain.Interfaces
{
    public interface IPortfolioStockRepository
    {
        Task<PortfolioStock> GetByPortfolioIdAndStockName(int portfolioId, string stockName);
        Task Insert(PortfolioStock portfolioStock);
    }
}
