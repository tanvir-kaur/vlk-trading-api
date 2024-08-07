using VLK.Trading.Domain.Models;

namespace VLK.Trading.Domain.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<Portfolio> GetPortfolioByUserId(Guid userId);
    }
}
