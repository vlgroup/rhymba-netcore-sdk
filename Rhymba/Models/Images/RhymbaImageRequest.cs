namespace Rhymba.Models.Images
{
    public class RhymbaImageRequest
    {
        public string imageId { get; set; } = string.Empty;
        public RhymbaImageOptions? imageOptions { get; set; }
    }
}
