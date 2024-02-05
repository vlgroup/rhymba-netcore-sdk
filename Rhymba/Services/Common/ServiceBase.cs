namespace Rhymba.Services.Common
{
    public class ServiceBase
    {
        protected readonly string rhymbaAccessToken;
        protected readonly string rhymbaAccessSecret;
        protected readonly HttpClient httpClient;

        protected ServiceBase(string rhymbaAccessToken, string rhymbaAccessSecret, HttpClient httpClient)
        {
            this.rhymbaAccessToken = rhymbaAccessToken;
            this.rhymbaAccessSecret = rhymbaAccessSecret;
            this.httpClient = httpClient;
        }
    }
}
