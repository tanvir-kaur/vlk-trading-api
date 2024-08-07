namespace VLK.Trading.Application.DTOModels
{
    public class StockDTO
    {
        public string Name { get; set; }
        public string TickerSymbol { get; set; }
        public string Currency { get; set; }
        public string ExchangeName { get; set; }
        public string ExchangeCode { get; set; }
        public string InstrumentType { get; set; }
        public decimal Price { get; set; }
        public DateTime LastTradeDateTime { get; set; }
        public bool IsMarketOpen { get; set; }
    }
}
