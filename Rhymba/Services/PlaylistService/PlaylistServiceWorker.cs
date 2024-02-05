namespace Rhymba.Services.Playlist
{
    using Rhymba.Models.Common;
    using Rhymba.Models.Playlist;
    using Rhymba.Services.Common;
    using System.Reflection;
    using System.Text;
    using System.Text.Json;

    public abstract class PlaylistServiceWorker : HashAuthenticatedServiceWorker
    {
        private readonly HttpClient httpClient;

        protected PlaylistServiceWorker(string rhymbaAccessToken, string rhymbaAccessSecret, string playlistPublicKey, string playlistPrivateKey, HttpClient httpClient) : base("https://playlist.mcnemanager.com", rhymbaAccessToken, rhymbaAccessSecret, playlistPublicKey, playlistPrivateKey)
        {
            this.httpClient = httpClient;
        }

        protected async Task<PlaylistResponseBase<TResponseType>?> CallPlaylistService<TResponseType>(PlaylistRequestBase request, string method, string route)
        {
            var requestHash = base.CreateAuthenticationHash(request, method, route);

            if (string.IsNullOrWhiteSpace(requestHash))
            {
                return default;
            }

            var url = $"{this.rhymbaUrl}{route}";

            this.httpClient.DefaultRequestHeaders.Clear();
            this.httpClient.DefaultRequestHeaders.Add("User-Agent", "rhymba_net_sdk");
            this.httpClient.DefaultRequestHeaders.Add("system_access_token", base.rhymbaAccessToken);
            this.httpClient.DefaultRequestHeaders.Add("system_access_secret", base.rhymbaAccessSecret);
            this.httpClient.DefaultRequestHeaders.Add("hash", requestHash);
            this.httpClient.DefaultRequestHeaders.Add("access_token", request.access_token);

            var queryParams = this.CreateQueryString(request);
            var formData = this.CreatePostBody(request);

            if (queryParams.Any())
            {
                url += $"?{string.Join("&", queryParams)}";
            }

            var responseData = string.Empty;
            if (method.ToLower() == "get")
            {
                responseData = await this.httpClient.GetStringAsync(url);
            }
            else if (method.ToLower() == "post")
            {
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json; charset=utf-8");
                var response = await this.httpClient.PostAsync(url, formData.Any() ? new FormUrlEncodedContent(formData) : null);
                if (response != null && response.IsSuccessStatusCode)
                {
                    responseData = await response.Content.ReadAsStringAsync();
                }
            }
            else if (method.ToLower() == "put")
            {
                var response = await this.httpClient.PutAsync(url, formData.Any() ? new FormUrlEncodedContent(formData) : null);
                if (response != null && response.IsSuccessStatusCode)
                {
                    responseData = await response.Content.ReadAsStringAsync();
                }
            }
            else if (method.ToLower() == "delete")
            {
                var response = await this.httpClient.DeleteAsync(url);
                if (response != null && response.IsSuccessStatusCode)
                {
                    responseData = await response.Content.ReadAsStringAsync();
                }
            }

            if (!string.IsNullOrWhiteSpace(responseData))
            {
                return JsonSerializer.Deserialize<PlaylistResponseBase<TResponseType>>(responseData, new JsonSerializerOptions() { NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString | System.Text.Json.Serialization.JsonNumberHandling.WriteAsString });
            }

            return default;
        }

        protected async Task<PlaylistResponseBase<TResponse>?> CallPlaylistAccountService<TRequest, TResponse>(TRequest request, string method, string route)
        {
            var requestHash = base.CreateAuthenticationHash(request, method, route);

            if (string.IsNullOrWhiteSpace(requestHash))
            {
                return default;
            }

            var url = $"{this.rhymbaUrl}{route}";

            this.httpClient.DefaultRequestHeaders.Clear();
            this.httpClient.DefaultRequestHeaders.Add("User-Agent", "rhymba_net_sdk");
            this.httpClient.DefaultRequestHeaders.Add("system_access_token", base.rhymbaAccessToken);
            this.httpClient.DefaultRequestHeaders.Add("system_access_secret", base.rhymbaAccessSecret);
            this.httpClient.DefaultRequestHeaders.Add("hash", requestHash);
            this.httpClient.DefaultRequestHeaders.Add("Accept", "application/json; charset=utf-8");

            var formData = this.CreatePostBody(request);

            if (typeof(TRequest) == typeof(AccountLoginRequest))
            {
                var loginRequest = (request as AccountLoginRequest);
                if (loginRequest != null)
                {
                    var authHeaderBytes = Encoding.UTF8.GetBytes($"{loginRequest.id}:{loginRequest.password}");
                    this.httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(authHeaderBytes)}");
                }
            }

            var response = await this.httpClient.PostAsync(url, formData.Any() ? new FormUrlEncodedContent(formData) : null);
            if (response != null && response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace (responseData))
                {
                    return JsonSerializer.Deserialize<PlaylistResponseBase<TResponse>>(responseData, new JsonSerializerOptions() { NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString | System.Text.Json.Serialization.JsonNumberHandling.WriteAsString, Converters = { new PlaylistDateTimeConverter() } });
                }
            }

            return default;
        }

        private List<string> CreateQueryString(object? request)
        {
            var queryParams = new List<string>();

            if (request != null)
            {
                foreach (var property in request.GetType().GetProperties())
                {
                    var propertyValue = property.GetValue(request);
                    if (propertyValue != null)
                    {
                        var bodyContentAttribute = property.GetCustomAttribute<BodyContent>();
                        if (bodyContentAttribute == null)
                        {
                            queryParams.Add($"{property.Name}={propertyValue}");
                        }
                    }
                }
            }

            return queryParams;
        }

        private List<KeyValuePair<string, string>> CreatePostBody(object? request)
        {
            var data = new List<KeyValuePair<string, string>>();

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
                            data.Add(new KeyValuePair<string, string>(property.Name, propertyValue.ToString() ?? string.Empty));
                        }
                    }
                }
            }

            return data;
        }
    }
}
