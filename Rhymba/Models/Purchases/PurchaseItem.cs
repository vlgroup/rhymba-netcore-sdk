namespace Rhymba.Models.Purchases
{
    public enum ContentType
    {
        Media = 0,
        Album
    }

    public enum SaleType
    {
        DigitialDownload = 0,
        Playlist,
        Ringtone,
        PromotionalGiveaway,
        NoncompulsoryStream,
        CompulsoryStream
    }

    public enum DeliveryType
    {
        Mobile = 0,
        Kiosk,
        Web,
        Stream
    }

    public enum ServiceType
    {
        PayPerUse = 0,
        Subscription = 1
    }

    public class PurchaseItem
    {
        public ContentType contenttype { get; set; }
        public int productid { get; set; }
        public decimal retail { get; set; }
        public decimal tax { get; set; }
        public decimal discountamount { get; set; }
        public SaleType saletype { get; set; }
        public DeliveryType deliverytype { get; set; }
        public string? promotiondescription { get; set; }
        public ServiceType servicetype { get; set; }
        public int quantity { get; set; }
    }
}
