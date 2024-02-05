namespace Rhymba.Services.Purchases
{
    using Rhymba.Models.Purchases;

    public class Reporting : PurchaseServiceWorker
    {
        internal Reporting(string accessToken, string accessSecret, HttpClient httpClient) : base(accessToken, accessSecret, httpClient)
        {

        }

        public async Task<FinalizePurchaseResponse?> FinalizePurchase(FinalizePurchaseRequest request)
        {
            return await this.CallPurchaseService<FinalizePurchaseRequest, FinalizePurchaseResponse>(request, "FinalizePurchase");
        }

        public async Task<ReportPurchaseResponse?> ReportPurchase(ReportPurchaseRequest request)
        {
            return await this.CallPurchaseService<ReportPurchaseRequest, ReportPurchaseResponse>(request, "ReportPurchase");
        }
    }
}
