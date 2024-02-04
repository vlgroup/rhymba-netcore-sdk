namespace Rhymba.Models.Playlist
{
    using Rhymba.Models.Common;

    public class AccountRegisterRequest
    {
        [BodyContent]
        public string password { get; set; } = string.Empty;
    }
}
