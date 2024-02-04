namespace Rhymba.Models.Playlist
{
    using Rhymba.Models.Common;

    public class AccountLoginRequest
    {
        [BodyContent]
        public int id { get; set; }
        [BodyContent]
        public string password { get; set; } = string.Empty;
    }
}
