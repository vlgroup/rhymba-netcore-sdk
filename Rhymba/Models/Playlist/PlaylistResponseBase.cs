namespace Rhymba.Models.Playlist
{
    public class PlaylistResponseBase
    {
        public bool success { get; set; }
        public string message { get; set; } = string.Empty;
        public int statusCode { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }
    }

    public class PlaylistResponseBase<T> : PlaylistResponseBase
    {
        public T? data { get; set; }
    }
}
