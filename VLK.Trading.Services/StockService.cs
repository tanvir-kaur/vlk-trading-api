using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLK.Trading.Core.Interfaces;
using VLK.Trading.Core.Models;
using VLK.Trading.Services.Interfaces;

namespace VLK.Trading.Services
{
    public class StockService : IStockService
    {
        public IUnitOfWork _unitOfWork;

        public StockService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Stock>> GetAllStocksAsync()
        {
            List<Stock> stockDetails = await _unitOfWork.Stocks.GetAllStocks();
            return stockDetails;
        }

        public async Task<Stock> GetStockDetailsByName(string stockName)
        {
            Stock stock = await _unitOfWork.Stocks.GetStockDetailsByName(stockName);
            return stock;
        }

        //public async Task<string> BuyStockAsync(int userId, int buyingStockUnit, string stockName)
        //{
        //    PortfolioUser portfolioUser = await _stockMarketDbContext.PortfolioUser
        //        .Include(portfolioUser => portfolioUser.User)
        //        .Include(portfolioUser => portfolioUser.Portfolio)
        //        .FirstOrDefaultAsync(portfolioUser => portfolioUser.UserId == userId);
        //    if (portfolioUser == null)
        //    {
        //        Log.Warning($"StockService-BuyStockAsync Hata: {userId} id li bir kullanıcı yok.");
        //        throw new Exception($"StockService-BuyStockAsync Hata: {userId} id li bir kullanıcı yok.");
        //    }

        //    //order by kullanırken first or default içinde where tarzı bir yazım yapmak ef tarafından önerilmemektedir.
        //    StockPrice stockPrice = await _stockMarketDbContext.StockPrices
        //        .Include(stockPrices => stockPrices.Stock)
        //        .Where(stockPrice => stockPrice.Stock.Name == stockName && stockPrice.Stock.Status == false)
        //        .OrderByDescending(stockPrice => stockPrice.Date)
        //        .FirstOrDefaultAsync();

        //    if (stockPrice == null)
        //    {
        //        Log.Warning($"StockService-BuyStockAsync Hata: stockPrice bulunamadı." +
        //           $" {stockName} isimli hisse senedine ait veri olamayabilir veya hisse senedi işleme kapalı olabilir");
        //        throw new Exception($"StockService-BuyStockAsync Hata: portfolioStock bulunamadı." +
        //            $" {stockName} isimli hisse senedine ait veri olamayabilir veya hisse senedi işleme kapalı olabilir");
        //    }

        //    MainBoard mainBoard = await _stockMarketDbContext.MainBoards.FirstOrDefaultAsync();
        //    if (mainBoard == null)
        //    {
        //        Log.Warning($"StockService-BuyStockAsync Hata: MainBoard bulunamadı");
        //        throw new Exception("StockService-BuyStockAsync Hata: MainBoard bulunamadı");
        //    }

        //    // bu değer null olabilir, bir kullanıcı daha  önce o hisseden hiç almamış olabilir.
        //    PortfolioStock portfolioStock = await _stockMarketDbContext.PortfolioStock
        //        .Include(portfolioStock => portfolioStock.Stock)
        //        .FirstOrDefaultAsync(portfolioStock => portfolioStock.Stock.Name == stockName
        //        && portfolioStock.PortfolioId == portfolioUser.PortfolioId
        //        && portfolioStock.Stock.Status == false);

        //    //Kurallar - hisse adeti yeterli mi - komisyondan sonra kalan para yeterli mi
        //    int stockLeft = stockPrice.Stock.Unit - buyingStockUnit;
        //    if (stockLeft < 0)
        //    {
        //        Log.Warning($"StockService-BuyStockAsync Hata: Satın alabileceğiniz kadar hisse senedi yok." +
        //            $" Sistemdeki hisse senedi adeti: {stockPrice.Stock.Unit}" +
        //            $" Almak istediğini miktar: {buyingStockUnit}");
        //        throw new Exception("StockService-BuyStockAsync Hata: Satın alabileceğiniz kadar hisse senedi yok." +
        //            $" Sistemdeki hisse senedi adeti: {stockPrice.Stock.Unit}" +
        //            $" Almak istediğini miktar: {buyingStockUnit}");
        //    }

        //    float transactionMoney = stockPrice.Price * buyingStockUnit;
        //    float commissionAmount = transactionMoney * ((float)mainBoard.CommissionRate / 100);
        //    float remainingMoney = portfolioUser.Portfolio.Balance - transactionMoney - commissionAmount;
        //    if (remainingMoney < 0)
        //    {
        //        Log.Warning($" StockService-BuyStockAsync Hata:" +
        //            $" {transactionMoney} TL'lik işlem miktarı için ödemeniz gerek komisyon {commissionAmount} TL'dir." +
        //            $" İşlem sonrası hesabınızda kalan tutar {remainingMoney} TL'dir. Bakiye 0'ın altına düşemez!");
        //        throw new Exception($" StockService-BuyStockAsync Hata:" +
        //            $" {transactionMoney} TL'lik işlem miktarı için ödemeniz gerek komisyon {commissionAmount} TL'dir." +
        //            $" İşlem sonrası hesabınızda kalan tutar {remainingMoney} TL'dir. Bakiye 0'ın altına düşemez!");
        //    }

        //    mainBoard.Balance += commissionAmount; // komisyonu ekle
        //    float oldPortfolioBalance = portfolioUser.Portfolio.Balance;
        //    portfolioUser.Portfolio.Balance = portfolioUser.Portfolio.Balance - transactionMoney - commissionAmount; // ücreti al. komisyonu al.
        //    if (portfolioStock == null)
        //    {
        //        portfolioStock = new PortfolioStock
        //        {
        //            PortfolioId = portfolioUser.PortfolioId,
        //            StockId = stockPrice.Stock.Id,
        //            Unit = buyingStockUnit,
        //            IsTracked = false
        //        };
        //        await _stockMarketDbContext.PortfolioStock.AddAsync(portfolioStock);
        //    }
        //    else
        //    {
        //        portfolioStock.Unit += buyingStockUnit;  // portföyümseki ilgili hisse adeti arttırıldı.
        //    }
        //    stockPrice.Stock.Unit = stockPrice.Stock.Unit - buyingStockUnit; // sistemdeki hisse senedi adeti eksiltildi.

        //    //veri tabanına detaylı kaydı ekleme.
        //    await _stockMarketDbContext.StockBuyAndSale.AddAsync(new StockBuyAndSale
        //    {
        //        PortfolioId = portfolioUser.PortfolioId,
        //        StockId = stockPrice.Stock.Id,
        //        Unit = buyingStockUnit,
        //        Status = "Buy", //alım demek
        //        Price = stockPrice.Price, // kaç tl den aldım.
        //        Date = DateTime.Now
        //    });

        //    await _stockMarketDbContext.SaveChangesAsync();

        //    Log.Warning($"{portfolioStock.Stock.Name} kodlu hisse senedinden," +
        //        $" {stockPrice.Price} TL fiyatından, {buyingStockUnit} adet satın adlınız." +
        //        $" İşlem {transactionMoney} TL, komisyon {commissionAmount} TL tuttu." +
        //        $" Eski bakiyeniz {oldPortfolioBalance} TL.\nYeni bakiyeniz {portfolioUser.Portfolio.Balance} TL." +
        //        $" Sistemde kalan hisse senedi {portfolioStock.Stock.Unit} adettir.");
        //    return $"{portfolioStock.Stock.Name} kodlu hisse senedinden,\n" +
        //        $" {stockPrice.Price} TL fiyatından, {buyingStockUnit} adet satın adlınız.\n" +
        //        $" İşlem {transactionMoney} TL, komisyon {commissionAmount} TL tuttu.\n" +
        //        $" Eski bakiyeniz {oldPortfolioBalance} TL.\nYeni bakiyeniz {portfolioUser.Portfolio.Balance} TL.\n" +
        //        $" Sistemde kalan hisse senedi {portfolioStock.Stock.Unit} adettir.\n";

        //}

    }
}
