using AutoMapper;
using Microsoft.Extensions.Logging;
using VLK.Trading.Application.DTOModels;
using VLK.Trading.Application.Interfaces;
using VLK.Trading.Domain.Interfaces;
using VLK.Trading.Domain.Models;

namespace VLK.Trading.Application
{
    public class PortfolioService : IPortfolioService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PortfolioService> _logger;

        public PortfolioService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PortfolioService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<UserPortfolioDTO> GetPortfolioByUserIdAsync(Guid userId)
        {
            try
            {
                Portfolio portfolio = await _unitOfWork.Portfolios.GetPortfolioByUserId(userId);
                return this._mapper.Map<UserPortfolioDTO>(portfolio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
