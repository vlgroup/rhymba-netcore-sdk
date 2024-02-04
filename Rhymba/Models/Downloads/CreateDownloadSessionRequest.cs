namespace Rhymba.Models.Downloads
{
    public class CreateDownloadSessionRequest
    {
        public int[]? albids { get; set; }
        public string? codeid { get; set; }
        public string countryCode { get; set; } = string.Empty;
        public int? ttlSeconds { get; set; }
        public int? downloadLimit { get; set; }
        public int[]? mids { get; set; }
        public string? other { get; set; }
        public int? playlistId { get; set; }
        public string? purchaseId { get; set; }
        public bool test { get; set; }
        public string? trackingId { get; set; }
        public string? userId { get; set; }
        public string? zip { get; set; }
        public string? dirName { get; set; }
        public string? plName { get; set; }
        public string? expires { get; set; }
        public int maxUses { get; set; }
    }
}
