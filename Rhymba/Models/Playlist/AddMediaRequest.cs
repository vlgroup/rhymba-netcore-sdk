namespace Rhymba.Models.Playlist
{
    using Rhymba.Models.Common;

    public class AddMediaRequest : PlaylistRequestBase
    {
        [BodyContent]
        public string mediaIds { get; set; } = string.Empty;
        [BodyContent]
        public int? insertBehind { get; set; }
    }
}
