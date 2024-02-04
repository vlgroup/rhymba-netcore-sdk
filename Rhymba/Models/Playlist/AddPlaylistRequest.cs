namespace Rhymba.Models.Playlist
{
    using Rhymba.Models.Common;

    public class AddPlaylistRequest : PlaylistRequestBase
    {
        [BodyContent]
        public int id { get; set; }
        [BodyContent]
        public int playlistId { get; set; }
        [BodyContent]
        public int? insertBehind { get; set; }
    }
}
