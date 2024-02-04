namespace Rhymba.Models.Playlist
{
    using Rhymba.Models.Common;

    public class AddAlbumRequest : PlaylistRequestBase
    {
        [BodyContent]
        public int albumId { get; set; }
        [BodyContent]
        public int? insertBehind { get; set; }
    }
}
