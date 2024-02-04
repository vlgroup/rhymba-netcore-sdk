namespace Rhymba.Models.Playlist
{
    public class GetPlaylistRequest : PlaylistRequestBase
    {
        public int? pageNum {  get; set; }
        public int? pageSize { get; set; }
    }
}
