namespace Rhymba.Services.Content
{
    using Rhymba.Services.Common;

    public class ContentService : ServiceBase
    {
        private Codes? Codes;
        private Downloads? Downloads;
        private Preview? Preview;
        private Streaming? Streaming;

        internal ContentService(string rhymbaAccessToken, string rhymbaAccessSecret, HttpClient httpClient) : base(rhymbaAccessToken, rhymbaAccessSecret, httpClient)
        {

        }

        public Codes GetCodes()
        {
            return this.Codes ??= new Codes(base.rhymbaAccessToken, base.rhymbaAccessSecret, base.httpClient);
        }

        public Downloads GetDownloads()
        {
            return this.Downloads ??= new Downloads(base.rhymbaAccessToken, base.rhymbaAccessSecret, base.httpClient);
        }

        public Preview GetPreview()
        {
            return this.Preview ??= new Preview(base.rhymbaAccessToken, base.httpClient);
        }

        public Streaming GetStreaming()
        {
            return this.Streaming ??= new Streaming(base.rhymbaAccessToken, base.rhymbaAccessSecret, base.httpClient);
        }
    }
}
