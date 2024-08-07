using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLK.Trading.Core.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TickerSymbol { get; set; }
        public string Currency { get; set; }
        public string ExchangeName { get; set; }
        public string ExchangeCode { get; set; }
        public string InstrumentType { get; set; }
        public int Unit { get; set; }

        public virtual ICollection<StockBuyAndSale> StockBuyAndSales { get; set; }
        public virtual ICollection<StockPrice> StockPrices { get; set; }
        public virtual ICollection<PortfolioStock> PortfolioStocks { get; set; }
    }

    public static class StockModelBuilderExtension
    {
        public static void BuildStockModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("Stock");
            });
        }
    }
}
