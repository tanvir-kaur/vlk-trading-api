using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VLK.Trading.Domain.Models
{
    public class StockPrice
    {
        [Key]
        public int Id { get; set; }
        public int StockId { get; set; }
        public decimal Price { get; set; }
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
