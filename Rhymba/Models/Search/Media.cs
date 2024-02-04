namespace Rhymba.Models.Search
{
    public class Media
    {
        public string album_artist_name { get; set; } = string.Empty;
        public int album_artistid { get; set; }
        public int album_id { get; set; }
        public bool album_isexplicit { get; set; }
        public int album_label_id { get; set; }
        public string album_label_name { get; set; } = string.Empty;
        public string album_name { get; set; } = string.Empty;
        public int album_numberoftracks { get; set; }
        public int album_provider_id { get; set; }
        public string album_provider_name { get; set; } = string.Empty;
        public string album_providerspecid { get; set; } = string.Empty;
        public DateTime album_releasedate { get; set; }
        public int album_status_id { get; set; }
        public DateTime album_streetdate { get; set; }
        public string album_upc { get; set; } = string.Empty;
        public int album_volumenumber { get; set; }
        public DateTime artist_dateadded { get; set; }
        public DateTime artist_dateupdated { get; set; }
        public int artist_id { get; set; }
        public string artist_name { get; set; } = string.Empty;
        public string basefile { get; set; } = string.Empty;
        public int bitrate { get; set; }
        public DateTime dateadded { get; set; }
        public int filesize { get; set; }
        public int format_id { get; set; }
        public Geos[]? geos { get; set; }
        public Genre? genre { get; set; }
        public int id { get; set; }
        public bool isexplicit { get; set; }
        public string isrc { get; set; } = string.Empty;
        public int length { get; set; }
        public int popularity_score_month { get; set; }
        public int popularity_score_ever { get; set; }
        public int provider_id { get; set; }
        public string provider_name { get; set; } = string.Empty;
        public string providerspecid { get; set; } = string.Empty;
        public DateTime releasedate { get; set; }
        public string relevant_title { get; set; } = string.Empty;
        public decimal score { get; set; }
        public Media[]? similar_results { get; set; }
        public int similar_results_count { get; set; }
        public int statusid { get; set; }
        public DateTime streetdate { get; set; }
        public string title { get; set; } = string.Empty;
        public int tracknumber { get; set; }
        public int volumenumber { get; set; }
    }
}
