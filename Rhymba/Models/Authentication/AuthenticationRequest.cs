namespace Rhymba.Models.Authentication
{
    internal class AuthenticationRequest<T>
    {
        public string access_token { get; set; } = string.Empty;
        public string access_secret { get; set; } = string.Empty;
        public string method { get; set; } = string.Empty;
        public int ttl { get; set; }
        public int use_limt { get; set; }
        public T? request { get; set; }
    }
}
