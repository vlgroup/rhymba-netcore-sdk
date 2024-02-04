namespace Rhymba.Models.Purchases
{
    using Rhymba.Models.Common;

    public enum ConfirmFlag
    {
        None = 0,
        Confirm = 1,
    }

    public class ReportPurchaseRequest
    {
        public string externalUserId { get; set; } = string.Empty;
        public string zip { get; set; } = string.Empty;
        public string currencyCode { get; set; } = string.Empty;
        public string countryCode { get; set; } = string.Empty;
        public string affiliateName { get; set; } = string.Empty;
        public string affiliateTransactionId { get; set; } = string.Empty;
        public bool isTest { get; set; }
        public DateTime saleDate { get; set; }
        public ConfirmFlag confirm { get; set; }

        [BodyContent]
        public PurchaseItem[]? purchasedItems { get; set; }
    }
}
