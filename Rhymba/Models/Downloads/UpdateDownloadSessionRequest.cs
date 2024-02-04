namespace Rhymba.Models.Downloads
{
    public class UpdateDownloadSessionRequest
    {
        public string token { get; set; } = string.Empty;
        public int? ttlSeconds { get; set; }
        public int? downloadLimit { get; set; }
        public string? dirName { get; set; }
        public string? plName { get; set; }
        public string? expires { get; set; }
        public int? maxUses { get; set; }
    }
}
