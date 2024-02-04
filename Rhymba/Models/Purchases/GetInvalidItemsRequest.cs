namespace Rhymba.Models.Purchases
{
    using Rhymba.Models.Common;

    public class GetInvalidItemsRequest
    {
        public string countryCode { get; set; } = string.Empty;
        [BodyContent]
        public PurchaseItem[]? purchasedItems { get; set; }
    }
}
