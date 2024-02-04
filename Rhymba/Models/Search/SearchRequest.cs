namespace Rhymba.Models.Search
{
    public class SearchRequest
    {
        public string[]? expand { get; set; }
        public string? filter { get; set; }
        public int[]? id_cdl { get; set; }
        public int? id { get; set; }
        public bool inlinecount { get; set; }
        public string keyword { get; set; } = string.Empty;
        public string[]? select { get; set; }
        public int? skip { get; set; }
        public int? top { get; set; }
        public bool full_search { get; set; }
    }
}
