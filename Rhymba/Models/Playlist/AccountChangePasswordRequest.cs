namespace Rhymba.Models.Playlist
{
    using Rhymba.Models.Common;

    public class AccountChangePasswordRequest
    {
        [BodyContent]
        public string access_token { get; set; } = string.Empty;
        [BodyContent]
        public string password { get; set; } = string.Empty;
    }
}
