namespace Rhymba.Services.Content
{
    using Rhymba.Models.Previews;

    public class Preview : ContentServiceWorker
    {
        internal Preview(string accessToken, HttpClient httpClient) : base(accessToken, string.Empty, httpClient)
        {

        }

        public string? GetPreviewUrl(GetPreviewRequest previewRequest)
        {
            return this.GetPreviewUrl(previewRequest.mediaId, previewRequest.filename, previewRequest.suid, previewRequest.luid, previewRequest.https);
        }

        public string? GetPreviewUrl(int mediaId, string? filename, string? suid, string? luid, bool? https)
        {
            if (string.IsNullOrWhiteSpace(this.rhymbaAccessToken))
            {
                return null;
            }

            if (mediaId == 0)
            {
                return null;
            }

            filename ??= $"{mediaId}.mp3";
            https ??= true;

            if (!filename.EndsWith(".mp3"))
            {
                filename += ".mp3";
            }

            var previewUrl = $"{this.rhymbaUrl}/preview/{mediaId}/{filename}/?access_token={this.rhymbaAccessToken}&https={https}";

            if (!string.IsNullOrWhiteSpace(suid))
            {
                previewUrl += $"&suid={suid}";
            }

            if (!string.IsNullOrWhiteSpace(luid))
            {
                previewUrl += $"&luid={luid}";
            }

            return previewUrl;
        }
    }
}
