using System.ComponentModel.DataAnnotations;

namespace VLK.Trading.API.Models
{
    public class BuyStockRequest
    {
        [Required]
        public int BuyingStockUnit { get; set; }
        [Required]
        public string TickerSymbol { get; set; }
    }
}
