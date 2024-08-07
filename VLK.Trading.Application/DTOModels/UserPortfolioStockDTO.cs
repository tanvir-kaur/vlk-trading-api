namespace VLK.Trading.Application.DTOModels
{
    public class UserPortfolioStockDTO
    {
        public string Name { get; set; }
        public string TickerSymbol { get; set; }
        public string Currency { get; set; }
        public string ExchangeName { get; set; }
        public string ExchangeCode { get; set; }
        public string InstrumentType { get; set; }
        public string Status { get; set; }
        public int Unit { get; set; }
        public decimal Price { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
