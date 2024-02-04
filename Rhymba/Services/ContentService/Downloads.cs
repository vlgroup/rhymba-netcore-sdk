namespace Rhymba.Services.Content
{
    using Rhymba.Models.Downloads;

    public class Downloads : ContentServiceWorker
    {
        internal Downloads(string accessToken, string accessSecret) : base(accessToken, accessSecret)
        {

        }

        public async Task<CreateDownloadSessionResponse?> CreateDownloadSession(CreateDownloadSessionRequest request)
        {
            if (request.albids == null && request.mids == null)
            {
                return null;
            }

            return await this.CallContentService<CreateDownloadSessionRequest, CreateDownloadSessionResponse>(request, "CreateDownloadSession");
        }

        public async Task<GetDownloadSessionInformationResponse?> GetDownloadSessionInformation(GetDownloadSessionInformationRequest request)
        {
            return await this.CallContentService<GetDownloadSessionInformationRequest, GetDownloadSessionInformationResponse>(request, "GetDownloadSessionInformation");
        }

        public async Task<string?> MarkDownloadSessionComplete(MarkDownloadSessionCompleteRequest request)
        {
            return await this.CallContentService<MarkDownloadSessionCompleteRequest, string>(request, "MarkSessionComplete");
        }

        public async Task<UpdateDownloadSessionResponse?> UpdateDownloadSession(UpdateDownloadSessionRequest request)
        {
            return await this.CallContentService<UpdateDownloadSessionRequest, UpdateDownloadSessionResponse>(request, "UpdateDownloadSession");
        }

        public string GenerateNakedMp3DownloadURL(string token, int mediaId, string filenme)
        {
            if (!filenme.EndsWith(".mp3"))
            {
                filenme += ".mp3";
            }

            return $"{base.rhymbaUrl}/download/{token}/{mediaId}/{filenme}";
        }

        public string GenerateZipDownloadURL(string token, string filename)
        {
            if (!filename.EndsWith(".zip"))
            {
                filename += ".zip";
            }

            return $"{base.rhymbaUrl}/dls/{token}/{filename}";
        }

        public string GenerateDLMDownloadURL(string token, string platfromExtension)
        {
            if (!platfromExtension.StartsWith("."))
            {
                platfromExtension = $".{platfromExtension}";
            }

            return $"{base.rhymbaUrl}/dlmin/DownloadManager_{token}_{platfromExtension}";
        }
    }
}
