namespace Rhymba.Services.Search
{
    using Rhymba.Services.Common;

    public class SearchService : ServiceBase
    {
        private AlbumsSearcher? Albums;
        private ArtistsSearcher? Artists;
        private MediaSearcher? Media;

        internal SearchService(string rhymbaAccessToken, HttpClient httpClient) : base(rhymbaAccessToken, string.Empty, httpClient)
        {

        }

        public AlbumsSearcher GetAlbums()
        {
            return this.Albums ??= new AlbumsSearcher(base.rhymbaAccessToken, base.httpClient);
        }

        public ArtistsSearcher GetArtists()
        {
            return this.Artists ??= new ArtistsSearcher(base.rhymbaAccessToken, base.httpClient);
        }

        public MediaSearcher GetMedia()
        {
            return this.Media ??= new MediaSearcher(base.rhymbaAccessToken, base.httpClient);
        }

        public SearchRequestBuilder GetRequestBuilder()
        {
            return new SearchRequestBuilder();
        }
    }
}
