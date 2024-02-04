namespace Rhymba.Models.Purchases
{
    public class GetInvalidItemsResponse
    {
        public InvalidItem[]? invaliditems { get; set; }
    }

    public class InvalidItem
    {
        public int id { get; set; }
        public string saletype { get; set; } = string.Empty;
        public string contenttype { get; set; } = string.Empty;
        public bool allowed { get; set; }
        public string? message { get; set; }
    }
}
