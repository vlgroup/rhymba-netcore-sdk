namespace Rhymba.Services.Images
{
    using Rhymba.Services.Common;

    public class ImageService : ServiceBase
    {
        private AlbumImage? AlbumImage;
        private RhymbaImage? RhymbaImage;

        internal ImageService() : base(string.Empty, string.Empty)
        { 

        }

        public AlbumImage GetAlbumImage()
        {
            return this.AlbumImage ??= new AlbumImage();
        }

        public RhymbaImage GetRhymbaImage()
        {
            return this.RhymbaImage ??= new RhymbaImage();
        }
    }
}
