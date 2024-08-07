using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VLK.Trading.Domain.Models
{
    public class PortfolioStock
    {
        [Key]
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public int StockId { get; set; }
        public int Unit { get; set; }
        public string Status { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Price { get; set; }
        public virtual Portfolio Portfolio { get; set; }
        public virtual Stock Stock { get; set; }
    }

    public static class PortfolioStockModelBuilderExtension
    {
        public static void BuildPortfolioStockModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PortfolioStock>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("PortfolioStock");
            });
        }
    }
}
