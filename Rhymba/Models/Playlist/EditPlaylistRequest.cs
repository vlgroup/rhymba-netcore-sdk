namespace Rhymba.Models.Playlist
{
    using Rhymba.Models.Common;

    public class EditPlaylistRequest : PlaylistRequestBase
    {
        [BodyContent]
        public string? name { get; set; }
        [BodyContent]
        public string? description { get; set; }
        [BodyContent]
        public bool? isRandom { get; set; }
        [BodyContent]
        public bool? Randomize { get; set; }
        [BodyContent]
        public bool? DMCA { get; set; }
        [BodyContent]
        public DateTime? startTime { get; set; }
    }
}
