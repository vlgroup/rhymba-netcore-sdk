namespace Rhymba.Services.Common
{
    public class ServiceBase
    {
        protected readonly string rhymbaAccessToken;
        protected readonly string rhymbaAccessSecret;

        protected ServiceBase(string rhymbaAccessToken, string rhymbaAccessSecret)
        {
            this.rhymbaAccessToken = rhymbaAccessToken;
            this.rhymbaAccessSecret = rhymbaAccessSecret;
        }
    }
}
