namespace Rhymba.Models.Images
{
    public class AlbumCoverRequest
    {
        public int albumId { get; set; }
        public RhymbaImageOptions? imageOptions { get; set; }
    }
}
