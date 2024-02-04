namespace Rhymba.Models.Playlist
{
    using Rhymba.Models.Common;

    public class EditPlaylistItemOrderRequest : PlaylistRequestBase
    {
        [BodyContent]
        public int? insertBehind { get; set; }
    }
}
