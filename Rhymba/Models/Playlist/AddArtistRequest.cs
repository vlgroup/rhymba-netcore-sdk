namespace Rhymba.Models.Playlist
{
    using Rhymba.Models.Common;

    public class AddArtistRequest : PlaylistRequestBase
    {
        public int artistId { get; set; }
        [BodyContent]
        public int? insertBehind { get; set; }
    }
}
