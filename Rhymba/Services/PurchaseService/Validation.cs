namespace Rhymba.Services.Purchases
{
    using Rhymba.Models.Purchases;

    public class Validation : PurchaseServiceWorker
    {
        internal Validation(string accessToken, string accessSecret, HttpClient httpClient) : base(accessToken, accessSecret, httpClient)
        {

        }

        public async Task<GetInvalidItemsResponse?> GetInvalidItems(GetInvalidItemsRequest request)
        {
            var invalidItems = await this.CallPurchaseService<GetInvalidItemsRequest, InvalidItem[]>(request, "GetInvalidItems");

            return new GetInvalidItemsResponse()
            {
                invaliditems = invalidItems
            };
        }
    }
}
