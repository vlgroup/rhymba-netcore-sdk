namespace Rhymba.Models.Codes
{
    public class CreateCreditCodeRequest
    {
        public Guid code {  get; set; }
        public string name { get; set; } = string.Empty;
        public decimal amount { get; set; }
        public DateTime created { get; set; }
        public DateTime? expires { get; set; }
        public bool enabled { get; set; }
        public string? notes { get; set; }
    }
}
