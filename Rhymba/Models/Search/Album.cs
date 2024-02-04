namespace Rhymba.Models.Search
{
    public class Album
    {
        public DateTime artist_dateadded { get; set; }
        public DateTime artist_dateupdated { get; set; }
        public int artist_id { get; set; }
        public string artist_name { get; set; } = string.Empty;
        public bool coverdeposited { get; set; }
        public Geos[]? geos { get; set; }
        public Genre[]? genres { get; set; }
        public int id { get; set; }
        public bool isexplicit { get; set; }
        public int label_id { get; set; }
        public string label_name { get; set; } = string.Empty;
        public decimal length { get; set; }
        public string name { get; set; } = string.Empty;
        public int numberoftracks { get; set; }
        public DateTime original_release_date { get; set; }
        public int provider_id { get; set; }
        public string provider_name { get; set; } = string.Empty;
        public string providerspecid { get; set; } = string.Empty;
        public DateTime releasedate { get; set; }
        public string relevant_name { get; set; } = string.Empty;
        public decimal score { get; set; }
        public int status_id { get; set; }
        public DateTime streetdate { get; set; }
        public string upc { get; set; } = string.Empty;
        public int volumenumber { get; set; }
    }
}
