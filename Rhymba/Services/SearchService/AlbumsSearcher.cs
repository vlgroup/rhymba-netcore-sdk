namespace Rhymba.Services.Search
{
    using Rhymba.Models.Search;

    public class AlbumsSearcher : SearchServiceWorker
    {
        internal AlbumsSearcher(string accessToken) : base(accessToken)
        {

        }

        public async Task<SearchResponse<Album>?> Search(SearchRequest request)
        {
            if (request.select == null)
            {
                request.select = new string[] { "artist_id", "artist_name" };
            }

            if (request.top == null || request.top < 1)
            {
                request.top = 25;
            }

            return await base.Search<Album>(request, "Albums");
        }

        public async Task<SearchResponse<Album>?> GetAlbum(int albumId)
        {
            var searchRequest = new SearchRequest()
            {
                id = albumId
            };

            return await this.Search(searchRequest);
        }

        public async Task<SearchResponse<Album>?> GetAlbumByArtistId(int artistId)
        {
            var searchRequest = new SearchRequest()
            {
                filter = $"artist_id eq {artistId}"
            };

            return await this.Search(searchRequest);
        }

        public async Task<SearchResponse<Album>?> GetAlbumByName(string name)
        {
            var searchRequest = new SearchRequest()
            {
                filter = $"name eq {name}"
            };

            return await this.Search(searchRequest);
        }

        public async Task<SearchResponse<Album>?> GetAlbumByKeyword(string keyword)
        {
            var searchRequest = new SearchRequest()
            {
                keyword = keyword
            };

            return await this.Search(searchRequest);
        }
    }
}
