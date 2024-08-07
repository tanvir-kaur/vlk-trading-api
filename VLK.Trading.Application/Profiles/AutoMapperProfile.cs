using AutoMapper;
using VLK.Trading.Application.DTOModels;
using VLK.Trading.Domain.Models;

namespace VLK.Trading.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Portfolio, UserPortfolioDTO>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Stocks, opt => opt.MapFrom(src => src.PortfoliosStocks));

            CreateMap<Stock, StockDTO>()
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.StockPrice.Price))
               .ForMember(dest => dest.LastTradeDateTime, opt => opt.MapFrom(src => src.StockPrice.LastTradeDateTime))
               .ForMember(dest => dest.IsMarketOpen, opt => opt.MapFrom(src => DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday));

            CreateMap<PortfolioStock, UserPortfolioStockDTO>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Stock.Name))
               .ForMember(dest => dest.TickerSymbol, opt => opt.MapFrom(src => src.Stock.TickerSymbol))
               .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Stock.Currency))
               .ForMember(dest => dest.ExchangeName, opt => opt.MapFrom(src => src.Stock.ExchangeName))
               .ForMember(dest => dest.ExchangeCode, opt => opt.MapFrom(src => src.Stock.ExchangeCode))
               .ForMember(dest => dest.InstrumentType, opt => opt.MapFrom(src => src.Stock.InstrumentType));

        }
    }
}
