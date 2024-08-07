using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLK.Trading.Core.Models
{
    public class StockBuyAndSale
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public int StockId { get; set; }
        public string Status { get; set; }
        public int Unit { get; set; }
        public float Price { get; set; }
        public DateTime TransactionDate { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual Portfolio Portfolio { get; set; }
    }

    public static class StockBuyAndSaleModelBuilderExtension
    {
        public static void BuildStockBuyAndSaleModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockBuyAndSale>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("StockBuyAndSale");
            });
        }
    }
}
