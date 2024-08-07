using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLK.Trading.Core.Models
{
    public class PortfolioStock
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public int StockId { get; set; }
        public int Unit { get; set; }

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
