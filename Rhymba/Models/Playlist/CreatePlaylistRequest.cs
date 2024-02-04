namespace Rhymba.Models.Playlist
{
    using Rhymba.Models.Common;

    public class CreatePlaylistRequest : PlaylistRequestBase
    {
        [BodyContent]
        public string name { get; set; } = string.Empty;
        [BodyContent]
        public string mediaIds { get; set; } = string.Empty;
    }
}
