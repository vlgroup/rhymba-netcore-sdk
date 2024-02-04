namespace Rhymba
{
    using Rhymba.Models.Credentials;
    using Rhymba.Services;

    public class RhymbaClient
    {
        private readonly ClientCredentials clientCredentials;

        private ServiceManager? ServiceManager;

        public RhymbaClient(string access_token, string access_secret) : this(new ClientCredentials(access_token, access_secret))
        {

        }

        public RhymbaClient(ClientCredentials clientCredentials)
        {
            this.clientCredentials = clientCredentials;
        }

        public ServiceManager GetServices()
        {
            return this.ServiceManager ??= new ServiceManager(this.clientCredentials.access_token, this.clientCredentials.access_secret);
        }
    }
}