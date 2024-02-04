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
        protected PlaylistServiceWorker(string rhymbaAccessToken, string rhymbaAccessSecret, string playlistPublicKey, string playlistPrivateKey) : base("https://playlist.mcnemanager.com", rhymbaAccessToken, rhymbaAccessSecret, playlistPublicKey, playlistPrivateKey)
        {

        }

        protected async Task<PlaylistResponseBase<TResponseType>?> CallPlaylistService<TResponseType>(PlaylistRequestBase request, string method, string route)
        {
            var requestHash = base.CreateAuthenticationHash(request, method, route);

            if (string.IsNullOrWhiteSpace(requestHash))
            {
                return default;
            }

            var url = $"{this.rhymbaUrl}{route}";

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "rhymba_net_sdk");
            httpClient.DefaultRequestHeaders.Add("system_access_token", base.rhymbaAccessToken);
            httpClient.DefaultRequestHeaders.Add("system_access_secret", base.rhymbaAccessSecret);
            httpClient.DefaultRequestHeaders.Add("hash", requestHash);
            httpClient.DefaultRequestHeaders.Add("access_token", request.access_token);

            var queryParams = this.CreateQueryString(request);
            var formData = this.CreatePostBody(request);

            if (queryParams.Any())
            {
                url += $"?{string.Join("&", queryParams)}";
            }

            var responseData = string.Empty;
            if (method.ToLower() == "get")
            {
                responseData = await httpClient.GetStringAsync(url);
            }
            else if (method.ToLower() == "post")
            {
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json; charset=utf-8");
                var response = await httpClient.PostAsync(url, formData.Any() ? new FormUrlEncodedContent(formData) : null);
                if (response != null && response.IsSuccessStatusCode)
                {
                    responseData = await response.Content.ReadAsStringAsync();
                }
            }
            else if (method.ToLower() == "put")
            {
                var response = await httpClient.PutAsync(url, formData.Any() ? new FormUrlEncodedContent(formData) : null);
                if (response != null && response.IsSuccessStatusCode)
                {
                    responseData = await response.Content.ReadAsStringAsync();
                }
            }
            else if (method.ToLower() == "delete")
            {
                var response = await httpClient.DeleteAsync(url);
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

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "rhymba_net_sdk");
            httpClient.DefaultRequestHeaders.Add("system_access_token", base.rhymbaAccessToken);
            httpClient.DefaultRequestHeaders.Add("system_access_secret", base.rhymbaAccessSecret);
            httpClient.DefaultRequestHeaders.Add("hash", requestHash);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json; charset=utf-8");

            var formData = this.CreatePostBody(request);

            if (typeof(TRequest) == typeof(AccountLoginRequest))
            {
                var loginRequest = (request as AccountLoginRequest);
                if (loginRequest != null)
                {
                    var authHeaderBytes = Encoding.UTF8.GetBytes($"{loginRequest.id}:{loginRequest.password}");
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(authHeaderBytes)}");
                }
            }

            var response = await httpClient.PostAsync(url, formData.Any() ? new FormUrlEncodedContent(formData) : null);
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
