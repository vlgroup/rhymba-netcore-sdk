namespace Rhymba.Models.Playlist
{
    public class PlaylistItem
    {
        public int MediaId { get; set; }
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public string TrackName { get; set; } = string.Empty;
        public string AlbumName { get; set; } = string.Empty;
        public string ArtistName { get; set; } = string.Empty;
        public int TrackLength { get; set; }
        public int id { get; set; }
        public int order { get; set; }
    }
}
