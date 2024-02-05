namespace Rhymba.Services.Content
{
    using Rhymba.Models.Common;
    using Rhymba.Services.Common;
    using System.Text.Json;

    public abstract class ContentServiceWorker : AuthenticatedServiceWorker
    {
        private readonly HttpClient httpClient;

        protected ContentServiceWorker(string rhymbaAccessToken, string rhymbaAccessSecret, HttpClient httpClient) : base("https://dispatch.mcnemanager.com/current/", rhymbaAccessToken, rhymbaAccessSecret)
        {
            this.httpClient = httpClient;
        }

        protected async Task<TResponseType?> CallContentService<TRequest, TResponseType>(TRequest request, string method, string? scope = null)
        {
            var contentToken = await this.CreateAuthenticationToken(request, scope ?? method);

            if (contentToken == null)
            {
                return default;
            }

            var url = $"{this.rhymbaUrl}content.odata/{method}/?$format=json";

            if (request != null)
            {
                foreach (var property in request.GetType().GetProperties())
                {
                    var propertyValue = property.GetValue(request);
                    if (propertyValue != null)
                    {
                        url = ODataUrl.AddQueryParam(url, property.Name, propertyValue);
                    }
                }

                foreach (var field in request.GetType().GetFields())
                {
                    var fieldValue = field.GetValue(request);
                    if (fieldValue != null)
                    {
                        url = ODataUrl.AddQueryParam(url, field.Name, fieldValue);
                    }
                }
            }

            this.httpClient.DefaultRequestHeaders.Clear();
            this.httpClient.DefaultRequestHeaders.Add("User-Agent", "rhymba_net_sdk");
            this.httpClient.DefaultRequestHeaders.Add("access_token", contentToken.access_token);
            this.httpClient.DefaultRequestHeaders.Add("access_hint", contentToken.access_hint);
            this.httpClient.DefaultRequestHeaders.Add("access_req", contentToken.access_req);

            var responseData = await this.httpClient.GetStringAsync(url);

            if (string.IsNullOrWhiteSpace(responseData))
            {
                return default;
            }

            if (responseData.Contains("\"value\":"))
            {
                var result = JsonSerializer.Deserialize<ODataValueWrapper<TResponseType>>(responseData, new JsonSerializerOptions() { NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString | System.Text.Json.Serialization.JsonNumberHandling.WriteAsString });
                if (result != null)
                {
                    return result.value;
                }

                return default;
            }
            else
            {
                return JsonSerializer.Deserialize<TResponseType>(responseData, new JsonSerializerOptions() { NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString | System.Text.Json.Serialization.JsonNumberHandling.WriteAsString });
            }
        }
    }
}
