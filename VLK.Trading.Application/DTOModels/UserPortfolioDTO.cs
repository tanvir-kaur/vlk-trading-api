namespace VLK.Trading.Application.DTOModels
{
    public class UserPortfolioDTO
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
        public decimal? Investments { get; set; } = 0;
        public string Currency { get; set; }
        public int AccountNumber { get; set; }
        public decimal CommissionRate { get; set; }
        public List<UserPortfolioStockDTO> Stocks { get; set; }
    }
}
