namespace Rhymba.Models.Playlist
{
    using Rhymba.Models.Common;

    public class AddUserToPlaylistRequest : PlaylistRequestBase
    {
        [BodyContent]
        public int userId { get; set; }
    }
}
