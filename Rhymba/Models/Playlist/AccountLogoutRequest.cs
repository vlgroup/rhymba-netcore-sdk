namespace Rhymba.Models.Playlist
{
    using Rhymba.Models.Common;

    public class AccountLogoutRequest
    {
        [BodyContent]
        public string access_token { get; set; } = string.Empty;
    }
}
