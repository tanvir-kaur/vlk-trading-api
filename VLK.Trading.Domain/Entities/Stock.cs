using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VLK.Trading.Domain.Models
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string TickerSymbol { get; set; }
        public string Currency { get; set; }
        public string ExchangeName { get; set; }
        public string ExchangeCode { get; set; }
        public string InstrumentType { get; set; }
        public virtual StockPrice StockPrice { get; set; }
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
