using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLK.Trading.Core.Models
{
    public class Portfolio
    {
        public int Id { get; set; }
        public float Balance { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<StockBuyAndSale> StockBuyAndSales { get; set; }
        public virtual ICollection<PortfolioStock> PortfoliosStocks { get; set; }
    }

    public static class PortfolioModelBuilderExtension
    {
        public static void BuildPortfolioModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Portfolio>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("Portfolio");
            });
        }
    }
}
