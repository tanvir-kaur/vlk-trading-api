using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace VLK.Trading.Domain.Models
{
    public class Portfolio
    {
        [Key]
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public decimal? Investments { get; set; } 
        public string Currency { get; set; }
        public Guid UserId { get; set; }
        public int AccountNumber { get; set; }
        public decimal CommissionRate { get; set; }
        public virtual User User { get; set; }
        public virtual IEnumerable<PortfolioStock> PortfoliosStocks { get; set; }
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
