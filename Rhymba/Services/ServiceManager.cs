namespace Rhymba.Services
{
    using Rhymba.Services.Content;
    using Rhymba.Services.Images;
    using Rhymba.Services.Playlist;
    using Rhymba.Services.Purchases;
    using Rhymba.Services.Search;

    public class ServiceManager
    {
        private readonly string rhymbaAccessToken;
        private readonly string rhymbaAccessSecret;
        private readonly HttpClient httpClient;

        private ContentService? ContentService;
        private ImageService? ImageService;
        private PlaylistService? PlaylistService;
        private PurchaseService? PurchaseService;
        private SearchService? SearchService;

        internal ServiceManager(string rhymbaAccessToken, string rhymbaAccessSecret, Func<HttpClient> httpClientFactory)
        {
            this.rhymbaAccessToken = rhymbaAccessToken;
            this.rhymbaAccessSecret = rhymbaAccessSecret;

            this.httpClient = httpClientFactory();
        }

        public ContentService GetContentService()
        {
            return this.ContentService ??= new ContentService(this.rhymbaAccessToken, this.rhymbaAccessSecret, this.httpClient);
        }

        public ImageService GetRhymbaImageService()
        {
            return this.ImageService ??= new ImageService(this.httpClient);
        }

        public PlaylistService GetPlaylistService(string playlistPrivateKey)
        {
            return this.PlaylistService ??= new PlaylistService(this.rhymbaAccessToken, this.rhymbaAccessSecret, string.Empty, playlistPrivateKey, this.httpClient);
        }

        public PurchaseService GetPurchaseService()
        {
            return this.PurchaseService ??= new PurchaseService(this.rhymbaAccessToken, this.rhymbaAccessSecret, this.httpClient);
        }

        public SearchService GetSearchService()
        {
            return this.SearchService ??= new SearchService(this.rhymbaAccessToken, this.httpClient);
        }
    }
}
