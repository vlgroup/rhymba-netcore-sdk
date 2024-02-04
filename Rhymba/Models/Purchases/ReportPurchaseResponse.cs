namespace Rhymba.Models.Purchases
{
    public class ReportPurchaseResponse
    {
        public string purchaseid { get; set; } = string.Empty;
        public string token { get; set; } = string.Empty;
        public InvalidItem[]? invaliditems { get; set; }
    }
}
