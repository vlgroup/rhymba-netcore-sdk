namespace Rhymba.Models.Playlist
{
    using Rhymba.Models.Common;

    public class UploadImageRequest : PlaylistRequestBase
    {
        [BodyContent]
        public int playlistId { get; set; }
        [BodyContent]
        public FileInfo? image { get; set; }
    }
}
