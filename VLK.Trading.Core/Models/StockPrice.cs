using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLK.Trading.Core.Models
{
    public class StockPrice
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public float Price { get; set; }
        public DateTime LastTradeDateTime { get; set; }
        public virtual Stock Stock { get; set; }

    }

    public static class StockPriceModelBuilderExtension
    {
        public static void BuildStockPriceModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockPrice>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("StockPrice");
            });
        }
    }
}
