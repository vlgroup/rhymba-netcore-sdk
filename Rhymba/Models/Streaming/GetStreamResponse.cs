namespace Rhymba.Models.Streaming
{
    public class GetStreamResponse
    {
        public int id { get; set; }
        public string url_segment_one { get; set; } = string.Empty;
        public string url_segment_two { get; set; } = string.Empty;
    }
}
