namespace Rhymba.Models.Streaming
{
    public enum GetStreamEncoding
    {
        MP3 = 0,
        FLAC,
        HEAAC,
        AAC
    }

    public enum GetStreamProtocol
    {
        HTTP = 0,
        HLS
    }

    public abstract class GetStreamRequestBase
    {
        public int bitrate { get; set; }
        public GetStreamEncoding encoding { get; set; }
        public int fadeEnd { get; set; }
        public int fadeStart { get; set; }
        public bool https { get; set; }
        public string luid { get; set; } = string.Empty;
        public bool mono { get; set; }
        public GetStreamProtocol protocol { get; set; }
        public string suid { get; set; } = string.Empty;
        public int trimEnd { get; set; }
        public int trimStart { get; set; }
    }

    public class GetStreamRequest : GetStreamRequestBase
    {
        public int mediaId { get; set; }
    }

    public class GetStreamsRequest : GetStreamRequestBase
    {
        public int[]? mediaIds { get; set; }
    }

    public class GetPlaylistStreamRequest : GetStreamRequestBase
    {
        public int playlistId { get; set; }
    }
}
