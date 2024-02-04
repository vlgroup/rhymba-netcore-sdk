namespace Rhymba.Models.Downloads
{
    public class GetDownloadSessionInformationResponse
    {
        public string token { get; set; } = string.Empty;
        public int successfulServes { get; set; }
        public int failedServes { get; set;}
        public int resetAmount { get; set; }
        public int totalContentCount { get; set; }
    }
}
