namespace Rhymba.Services.Search
{
    using Rhymba.Models.Search;

    public class MediaSearcher : SearchServiceWorker
    {
        internal MediaSearcher(string accessToken) : base(accessToken)
        {

        }

        public async Task<SearchResponse<Media>?> Search(SearchRequest request)
        {
            if (request.select == null)
            {
                request.select = new string[] { "id", "title", "artist_id", "artist_name" };
            }

            if (request.top == null || request.top < 1)
            {
                request.top = 25;
            }

            return await base.Search<Media>(request, "Medias");
        }

        public async Task<SearchResponse<Media>?> GetMedia(int mediaId)
        {
            var searchRequest = new SearchRequest()
            {
                id = mediaId
            };

            return await this.Search(searchRequest);
        }

        public async Task<SearchResponse<Media>?> GetMediaByIdCdl(int[] id_cdl)
        {
            var searchRequest = new SearchRequest()
            {
                id_cdl = id_cdl
            };

            return await this.Search(searchRequest);
        }

        public async Task<SearchResponse<Media>?> GetMediaByKeyword(string keyword)
        {
            var searchRequest = new SearchRequest()
            {
                keyword = keyword
            };

            return await this.Search(searchRequest);
        }

        public async Task<SearchResponse<Media>?> GetMediaByArtistId(int artistId)
        {
            var searchRequest = new SearchRequest()
            {
                filter = $"artist_id eq {artistId}"
            };

            return await this.Search(searchRequest);
        }

        public async Task<SearchResponse<Media>?> GetMediaByArtistName(string artistName)
        {
            var searchRequest = new SearchRequest()
            {
                filter = $"artist_name eq {artistName}"
            };

            return await this.Search(searchRequest);
        }

        public async Task<SearchResponse<Media>?> GetMediaByTitle(string title)
        {
            var searchRequest = new SearchRequest()
            {
                filter = $"title eq {title}"
            };

            return await this.Search(searchRequest);
        }

        public async Task<SearchResponse<Media>?> GetMediaByAlbumId(int albumId)
        {
            var searchRequest = new SearchRequest()
            {
                filter = $"album_id eq {albumId}"
            };

            return await this.Search(searchRequest);
        }
    }
}
