namespace Rhymba.Services.Content
{
    using Rhymba.Models.Streaming;

    public class Streaming : ContentServiceWorker
    {
        internal Streaming(string accessToken, string accessSecret, HttpClient httpClient) : base(accessToken, accessSecret, httpClient)
        {

        }

        public async Task<GetStreamResponse?> GetStream(GetStreamRequest request)
        {
            return await this.CallContentService<GetStreamRequest, GetStreamResponse>(request, "GetStream", "GetStream");
        }

        public async Task<GetStreamResponse[]?> GetStreams(GetStreamsRequest request)
        {
            return await this.CallContentService<GetStreamsRequest, GetStreamResponse[]>(request, "GetStreams", "GetStream");
        }

        public async Task<GetStreamResponse[]?> GetPlaylistStream(GetPlaylistStreamRequest request)
        {
            return await this.CallContentService<GetPlaylistStreamRequest, GetStreamResponse[]>(request, "GetPlaylistStream", "GetStream");
        }
    }
}
