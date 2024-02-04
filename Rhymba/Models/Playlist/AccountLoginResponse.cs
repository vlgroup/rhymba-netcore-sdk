namespace Rhymba.Models.Playlist
{
    public class AccountLoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTime Expiration { get; set; }
    }
}
