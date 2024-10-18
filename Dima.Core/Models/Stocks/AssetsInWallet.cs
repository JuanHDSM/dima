namespace Dima.Core.Models.Stocks
{
    public class AssetsInWallet
    {
        public string UserId { get; set; } = string.Empty;
        public string logoUrl { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public decimal CurrentPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Average { get; set; }
        public decimal Balance { get; set; }
    }
}