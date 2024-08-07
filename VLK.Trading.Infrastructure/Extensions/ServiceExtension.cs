using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VLK.Trading.Infrastructure.Repositories;
using VLK.Trading.Domain.Interfaces;
using VLK.Trading.Application.Models;
using VLK.Trading.Infrastructure.Data;

namespace VLK.Trading.Infrastructure.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TradingDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("TradingDatabaseConnection"));
            });

            services.Configure<TokenInformation>(configuration.GetSection("JwtConfig"));
            services.AddOptions();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();
            services.AddScoped<IStocksRepository, StocksRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPortfolioStockRepository, PortfolioStockRepository>();

            return services;
        }
    }
}
