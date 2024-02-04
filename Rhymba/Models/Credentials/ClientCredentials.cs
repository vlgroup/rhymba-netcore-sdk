namespace Rhymba.Models.Credentials
{
    public class ClientCredentials
    {
        public string access_token { get; set; } = string.Empty;
        public string access_secret { get;set; } = string.Empty;

        public ClientCredentials()
        {

        }

        public ClientCredentials(string access_token, string access_secret)
        {
            this.access_token = access_token;
            this.access_secret = access_secret;
        }
    }
}
