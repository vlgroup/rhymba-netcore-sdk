namespace Rhymba.Models.Authentication
{
    public class AuthenticationResponse
    {
        public string access_token { get; set; } = string.Empty;
        public string access_req { get; set; } = string.Empty;
        public string access_hint { get; set; } = string.Empty;
    }
}
