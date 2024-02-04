namespace Rhymba.Services
{
    using Rhymba.Services.Common;
    using Rhymba.Services.Content;
    using Rhymba.Services.Images;
    using Rhymba.Services.Playlist;
    using Rhymba.Services.Purchases;
    using Rhymba.Services.Search;

    public class ServiceManager : ServiceBase
    {
        private ContentService? ContentService;
        private ImageService? ImageService;
        private PlaylistService? PlaylistService;
        private PurchaseService? PurchaseService;
        private SearchService? SearchService;

        internal ServiceManager(string rhymbaAccessToken, string rhymbaAccessSecret) : base(rhymbaAccessToken, rhymbaAccessSecret)
        {

        }

        public ContentService GetContentService()
        {
            return this.ContentService ??= new ContentService(this.rhymbaAccessToken, this.rhymbaAccessSecret);
        }

        public ImageService GetRhymbaImageService()
        {
            return this.ImageService ??= new ImageService();
        }

        public PlaylistService GetPlaylistService(string playlistPrivateKey)
        {
            return this.PlaylistService ??= new PlaylistService(this.rhymbaAccessToken, this.rhymbaAccessSecret, string.Empty, playlistPrivateKey);
        }

        public PurchaseService GetPurchaseService()
        {
            return this.PurchaseService ??= new PurchaseService(this.rhymbaAccessToken, this.rhymbaAccessSecret);
        }

        public SearchService GetSearchService()
        {
            return this.SearchService ??= new SearchService(this.rhymbaAccessToken);
        }
    }
}
