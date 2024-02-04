namespace Rhymba.Models.Codes
{
    public class CreateCreditCodeResponse
    {
        public int id { get; set; }
        public Guid code { get; set; }
        public int systemId { get; set; }
        public string name { get; set; } = string.Empty;
        public decimal amount { get; set; }
        public DateTime created { get; set; }
        public DateTime? expires { get; set; }
        public bool enabled { get; set; }
        public string? notes { get; set; }
    }
}
