namespace Rhymba.Services.Images
{
    using Rhymba.Models.Images;

    public class AlbumImage : ImageServiceWorker
    {
        internal AlbumImage()
        {

        }

        public string? GetAlbumCoverUrl(AlbumCoverRequest albumCoverRequest)
        {
            return this.BuildImageUrl(albumCoverRequest.albumId.ToString(), albumCoverRequest.imageOptions, "cover");
        }
    }
}
