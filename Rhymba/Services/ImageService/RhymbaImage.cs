namespace Rhymba.Services.Images
{
    using Rhymba.Models.Images;
    using System.Web;
    using static System.Net.Mime.MediaTypeNames;

    public class RhymbaImage : ImageServiceWorker
    {
        private readonly HttpClient httpClient;

        internal RhymbaImage(HttpClient httpClient) : base()
        {
            this.httpClient = httpClient;
        }

        public string? GetImageUrl(RhymbaImageRequest request)
        {
            return this.BuildImageUrl(request.imageId, request.imageOptions, "img");
        }

        public async Task<string?> PutImagge(RhymbaImageUploadRequest request)
        {
            if (request.image == null)
            {
                return null;
            }    

            var url = $"{this.rhymbaUrl}/new.imgup?account={request.accountId}&title={HttpUtility.UrlEncode(request.title)}&gallery={request.galleryId}&focusx={request.focusXPercent}&focusy={request.focusYPercent}&ext={request.image.Extension}";

            this.httpClient.DefaultRequestHeaders.Clear();
            this.httpClient.DefaultRequestHeaders.Add("User-Agent", "rhymba_net_sdk");

            var multipartContent = new MultipartFormDataContent();
            using (var fileStream = request.image.OpenRead())
            {
                var fileContent = new StreamContent(fileStream);
                multipartContent.Add(fileContent, "file", request.image.Name);

                var response = await this.httpClient.PostAsync(url, multipartContent);

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
