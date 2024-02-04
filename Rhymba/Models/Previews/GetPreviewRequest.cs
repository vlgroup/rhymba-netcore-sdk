namespace Rhymba.Models.Previews
{
    public class GetPreviewRequest
    {
        public int mediaId {  get; set; }
        public string? filename { get; set; }
        public string? suid { get; set; }
        public string? luid { get; set; }
        public bool? https { get; set; }
    }
}
