using AutoMapper;
using Microsoft.Extensions.Logging;
using VLK.Trading.Application.DTOModels;
using VLK.Trading.Application.Enums;
using VLK.Trading.Application.Exceptions;
using VLK.Trading.Application.Interfaces;
using VLK.Trading.Domain.Interfaces;
using VLK.Trading.Domain.Models;

namespace VLK.Trading.Application
{
    public class StocksService : IStocksService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<StocksService> _logger;

        public StocksService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<StocksService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<StockDTO>> GetAllStocksAsync()
        {
            try
            {
                List<Stock> stockDetails = await _unitOfWork.Stocks.GetAllStocks();
                return this._mapper.Map<List<StockDTO>>(stockDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<StockDTO> GetStockDetailsByTickerSymbolAsync(string tickerSymbol)
        {
            try
            {
                Stock stock = await _unitOfWork.Stocks.GetStockDetailsTickerSymbol(tickerSymbol);
                return this._mapper.Map<StockDTO>(stock);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task BuyStockAsync(Guid userId, int buyingStockUnit, string stockTicker)
        {
            try
            {
                var portfolioUser = await _unitOfWork.Portfolios.GetPortfolioByUserId(userId);
                if (portfolioUser == null)
                {
                    _logger.LogWarning($"StockService-BuyStockAsync: {userId} not found");
                    throw new ValidationException($"User not found");
                }

                Stock stockDetails = await _unitOfWork.Stocks.GetStockDetailsTickerSymbol(stockTicker);

                if (stockDetails == null)
                {
                    _logger.LogWarning($"StockService-BuyStockAsync: {stockTicker} may not have data or the stock may be closed for trading.");
                    throw new ValidationException($"{stockTicker} may not have data or the stock may be closed for trading.");
                }

                decimal transactionMoney = stockDetails.StockPrice.Price * buyingStockUnit;

                //TODO: Commission Rate can be saved in the database
                decimal commissionRate = 2;
                decimal commissionAmount = transactionMoney * ((decimal)commissionRate / 100);
                decimal remainingMoney = portfolioUser.Balance - transactionMoney - commissionAmount;
                if (remainingMoney < 0)
                {
                    _logger.LogWarning($" StockService-BuyStockAsync:" +
                        $" Insuffient balance transaction money: {transactionMoney} : Commission Amount {commissionAmount} : remaining money {remainingMoney} ");
                    throw new ValidationException($"Insuffient balance transaction money: {transactionMoney} : Commission Amount {commissionAmount} : remaining money {remainingMoney} ");
                }

                PortfolioStock portfolioStock = await _unitOfWork.PortfolioStocks.GetByPortfolioIdAndStockName(portfolioUser.Id, stockDetails.Name);

                //TODO: This table can be furthur normalized to track the transactions and if the stock was sold or bought
                if (portfolioStock == null)
                {
                    portfolioStock = new PortfolioStock
                    {
                        PortfolioId = portfolioUser.Id,
                        StockId = stockDetails.Id,
                        Unit = buyingStockUnit,
                        TransactionDate = DateTime.Now,
                        Price = stockDetails.StockPrice.Price,
                        Status = PortfolioStockStatusEnum.Executed.ToString()
                    };
                    await _unitOfWork.PortfolioStocks.Insert(portfolioStock);
                }
                else
                {
                    portfolioStock.Unit += buyingStockUnit;
                }
                portfolioUser.Balance = remainingMoney;
                portfolioUser.Investments = portfolioUser.Investments + transactionMoney;
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
