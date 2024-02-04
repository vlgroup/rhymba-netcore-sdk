namespace Rhymba.Services.Search
{
    using Rhymba.Models.Search;

    public class ArtistsSearcher : SearchServiceWorker
    {
        internal ArtistsSearcher(string accessToken) : base(accessToken)
        {

        }

        public async Task<SearchResponse<Artist>?> Search(SearchRequest request)
        {
            if (request.select == null)
            {
                request.select = new string[] { "id", "name" };
            }

            if (request.top == null || request.top < 1)
            {
                request.top = 25;
            }

            return await base.Search<Artist>(request, "Artists");
        }

        public async Task<SearchResponse<Artist>?> GetArtist(int artistId)
        {
            var searchRequest = new SearchRequest()
            {
                id = artistId
            };

            return await this.Search(searchRequest);
        }

        public async Task<SearchResponse<Artist>?> GetArtistByName(string name)
        {
            var searchRequest = new SearchRequest()
            {
                filter = $"name eq {name}"
            };

            return await this.Search(searchRequest);
        }

        public async Task<SearchResponse<Artist>?> GetArtistByKeyword(string keyword)
        {
            var searchRequest = new SearchRequest()
            {
                keyword = keyword
            };

            return await this.Search(searchRequest);
        }
    }
}
