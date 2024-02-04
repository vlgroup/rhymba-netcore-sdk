namespace Rhymba.Models.Search
{
    public class Geos
    {
        public string id { get; set; } = string.Empty;
        public int country_id { get; set; }
        public string country_name { get; set; } = string.Empty;
        public string country_code { get; set; } = string.Empty;
        public string currency { get; set; } = string.Empty;
        public int statusid { get; set; }
        public int domainsum { get; set; }
        public int pricecodeid { get; set; }
        public DateTime streetdate { get; set; }
        public DateTime releasedate { get; set; }
        public string pricecode { get; set; } = string.Empty;
        public decimal price { get; set; }
        public decimal maxretailprice { get; set; }
        public decimal surcharge { get; set; }
        public string currency_code { get; set; } = string.Empty;
    }
}
