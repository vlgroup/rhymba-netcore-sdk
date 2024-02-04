namespace Rhymba.Models.Playlist
{
    using Rhymba.Models.Common;

    public class CopyPlaylistRequest : PlaylistRequestBase
    {
        [BodyContent]
        public string name { get; set; } = string.Empty;
    }
}
