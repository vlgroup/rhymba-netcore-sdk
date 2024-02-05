namespace Rhymba.Services.Purchases
{
    using Rhymba.Services.Common;

    public class PurchaseService : ServiceBase
    {
        private Reporting? Reporting;
        private Validation? Validation;

        internal PurchaseService(string rhymbaAccessToken, string rhymbaAccessSecret, HttpClient httpClient) : base(rhymbaAccessToken, rhymbaAccessSecret, httpClient)
        {

        }

        public Reporting GetReporting()
        {
            return this.Reporting ??= new Reporting(base.rhymbaAccessToken, base.rhymbaAccessSecret, base.httpClient);
        }

        public Validation GetValidation()
        {
            return this.Validation ??= new Validation(base.rhymbaAccessToken, base.rhymbaAccessSecret, base.httpClient);
        }
    }
}
