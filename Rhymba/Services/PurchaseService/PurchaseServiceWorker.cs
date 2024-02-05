namespace Rhymba.Services.Purchases
{
    using Rhymba.Models.Common;
    using Rhymba.Services.Common;
    using System.Reflection;
    using System.Text.Json;

    public abstract class PurchaseServiceWorker : AuthenticatedServiceWorker
    {
        private readonly HttpClient httpClient;

        protected PurchaseServiceWorker(string rhymbaAccessToken, string rhymbaAccessSecret, HttpClient httpClient1) : base("https://purchases.mcnemanager.com/", rhymbaAccessToken, rhymbaAccessSecret)
        {
            this.httpClient = httpClient1;
        }

        protected async Task<TResponseType?> CallPurchaseService<TRequest, TResponseType>(TRequest request, string method)
        {
            var contentToken = await this.CreateAuthenticationToken(request, method);

            if (contentToken == null)
            {
                return default;
            }

            var url = $"{this.rhymbaUrl}Purchases.svc/{method}/?$format=json";
            var postBody = string.Empty;

            if (request != null)
            {
                foreach (var property in request.GetType().GetProperties())
                {
                    var propertyValue = property.GetValue(request);
                    if (propertyValue != null)
                    {
                        var bodyContentAttribute = property.GetCustomAttribute<BodyContent>();
                        if (bodyContentAttribute != null)
                        {
                            if (!string.IsNullOrWhiteSpace(postBody))
                            {
                                postBody += '&';
                            }

                            postBody += $"{property.Name}='{JsonSerializer.Serialize(propertyValue)}'";
                        }
                        else
                        {
                            url = ODataUrl.AddQueryParam(url, property.Name, propertyValue);
                        }
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

            var responseData = string.Empty;
            if (!string.IsNullOrWhiteSpace(postBody))
            {
                this.httpClient.DefaultRequestHeaders.Add("Accept", "application/json; charset=utf-8");
                var response = await this.httpClient.PostAsync(url, new StringContent(postBody));
                if (response != null)
                {
                    responseData = await response.Content.ReadAsStringAsync();
                }
            }
            else
            {
                responseData = await this.httpClient.GetStringAsync(url);
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
