using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VLK.Trading.Core.Interfaces;
using VLK.Trading.Core.Models;
using VLK.Trading.Services.Interfaces;

namespace VLK.Trading.Services
{
    public class PortfolioService : IPortfolioService
    {
        public IUnitOfWork _unitOfWork;

        public PortfolioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Portfolio> GetPortfolioDetails()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var userId = identity.Claims.Where(c => c.Type == "userId").Select(c => c.Value).SingleOrDefault();
            if (userId != null)
            {
                Guid userGuid = Guid.Parse(userId);
                Portfolio portfolio = await _unitOfWork.Portfolios.GetPortfolioByUserId(userGuid);
                return portfolio;
            }
            return new Portfolio();
        }
    }
}
