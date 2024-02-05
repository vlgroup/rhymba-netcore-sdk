namespace Rhymba.Services.Content
{
    using Rhymba.Models.Codes;

    public class Codes : ContentServiceWorker
    {
        internal Codes(string accessToken, string accessSecret, HttpClient httpClient) : base(accessToken, accessSecret, httpClient) 
        {

        }

        public async Task<CreateCreditCodeResponse?> CreateCode(CreateCreditCodeRequest request)
        {
            return await this.CallContentService<CreateCreditCodeRequest, CreateCreditCodeResponse>(request, "CreateCode");
        }

        public async Task<GetCodeResponse?> GetCode(GetCodeRequest request)
        {
            return await this.CallContentService<GetCodeRequest, GetCodeResponse>(request, "GetCode");
        }

        public async Task<string?> EditCode(EditCodeRequest request)
        {
            return await this.CallContentService<EditCodeRequest, string>(request, "EditCode");
        }

        public async Task<bool?> EditCodes(EditCodesRequest request)
        {
            return await this.CallContentService<EditCodesRequest, bool>(request, "EditCodes");
        }
    }
}
