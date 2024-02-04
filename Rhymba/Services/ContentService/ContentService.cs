namespace Rhymba.Services.Content
{
    using Rhymba.Services.Common;

    public class ContentService : ServiceBase
    {
        private Codes? Codes;
        private Downloads? Downloads;
        private Preview? Preview;
        private Streaming? Streaming;

        internal ContentService(string rhymbaAccessToken, string rhymbaAccessSecret) : base(rhymbaAccessToken, rhymbaAccessSecret)
        {

        }

        public Codes GetCodes()
        {
            return this.Codes ??= new Codes(this.rhymbaAccessToken, this.rhymbaAccessSecret);
        }

        public Downloads GetDownloads()
        {
            return this.Downloads ??= new Downloads(this.rhymbaAccessToken, this.rhymbaAccessSecret);
        }

        public Preview GetPreview()
        {
            return this.Preview ??= new Preview(this.rhymbaAccessToken);
        }

        public Streaming GetStreaming()
        {
            return this.Streaming ??= new Streaming(this.rhymbaAccessToken, this.rhymbaAccessSecret);
        }
    }
}
