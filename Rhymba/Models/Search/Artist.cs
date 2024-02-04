namespace Rhymba.Models.Search
{
    public class Artist
    {
        public DateTime catalog_update { get; set; }
        public string description { get; set; } = string.Empty;
        public string[]? genres { get; set; }
        public int id { get; set; }
        public string[]? influences { get; set; }
        public string language { get; set; } = string.Empty;
        public Genre[]? media_genres { get; set; }
        public Provider[]? media_providers { get; set; }
        public string name { get; set; } = string.Empty;
        public string plain_text_name { get; set; } = string.Empty;
        public decimal score { get; set; }
    }
}
