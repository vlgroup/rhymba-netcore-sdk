namespace Rhymba.Services.Search
{
    using Rhymba.Services.Common;

    public class SearchService : ServiceBase
    {
        private AlbumsSearcher? Albums;
        private ArtistsSearcher? Artists;
        private MediaSearcher? Media;

        internal SearchService(string rhymbaAccessToken) : base(rhymbaAccessToken, string.Empty)
        {

        }

        public AlbumsSearcher GetAlbums()
        {
            return this.Albums ??= new AlbumsSearcher(this.rhymbaAccessToken);
        }

        public ArtistsSearcher GetArtists()
        {
            return this.Artists ??= new ArtistsSearcher(this.rhymbaAccessToken);
        }

        public MediaSearcher GetMedia()
        {
            return this.Media ??= new MediaSearcher(this.rhymbaAccessToken);
        }

        public SearchRequestBuilder GetRequestBuilder()
        {
            return new SearchRequestBuilder();
        }
    }
}
