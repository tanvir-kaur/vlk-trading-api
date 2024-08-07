using Microsoft.EntityFrameworkCore;
using VLK.Trading.Domain.Models;

namespace VLK.Trading.Infrastructure.Data
{
    public class TradingDBContext : DbContext
    {
        public TradingDBContext(DbContextOptions<TradingDBContext> contextOptions) : base(contextOptions)
        {

        }

        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioStock> PortfolioStocks { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockPrice> StockPrices { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            BuildClientFilters(modelBuilder);

            modelBuilder.BuildUserModel();
            modelBuilder.BuildPortfolioModel();
            modelBuilder.BuildPortfolioStockModel();
            modelBuilder.BuildStockModel();
            modelBuilder.BuildStockPriceModel();
        }

        protected void BuildClientFilters(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(e => e.IsActive);
        }
    }
}
