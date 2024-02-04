namespace Rhymba.Services.Purchases
{
    using Rhymba.Services.Common;

    public class PurchaseService : ServiceBase
    {
        private Reporting? Reporting;
        private Validation? Validation;

        internal PurchaseService(string rhymbaAccessToken, string rhymbaAccessSecret) : base(rhymbaAccessToken, rhymbaAccessSecret)
        {

        }

        public Reporting GetReporting()
        {
            return this.Reporting ??= new Reporting(this.rhymbaAccessToken, this.rhymbaAccessSecret);
        }

        public Validation GetValidation()
        {
            return this.Validation ??= new Validation(this.rhymbaAccessToken, this.rhymbaAccessSecret);
        }
    }
}
