namespace Rhymba.Models.Images
{
    public class RhymbaImageUploadRequest
    {
        public int accountId { get; set; }
        public string title { get; set; } = string.Empty;
        public int galleryId { get; set; }
        public int focusXPercent { get; set; }
        public int focusYPercent { get; set; }
        public FileInfo? image { get; set; }
    }
}
