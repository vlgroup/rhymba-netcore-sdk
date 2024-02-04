namespace Rhymba
{
    using Rhymba.Models.Authentication;
    using Rhymba.Models.Common;
    using Rhymba.Services.Common;
    using System.Reflection;
    using System.Text.Json;

    internal class Authentication
    {
        internal static async Task<AuthenticationResponse?> CreateContentToken<T>(string serverUrl, AuthenticationRequest<T> authenticationRequest)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "token_agent");
            httpClient.DefaultRequestHeaders.Add("access_token", authenticationRequest.access_token);
            httpClient.DefaultRequestHeaders.Add("access_secret", authenticationRequest.access_secret);
            httpClient.DefaultRequestHeaders.Add("method", authenticationRequest.method);
            httpClient.DefaultRequestHeaders.Add("ttl", authenticationRequest.ttl.ToString());
            httpClient.DefaultRequestHeaders.Add("use_limit", authenticationRequest.use_limt.ToString());

            var rhymbaTokenUrl = $"{serverUrl}token.create";

            if (authenticationRequest.request != null)
            {
                foreach (var property in authenticationRequest.request.GetType().GetProperties())
                {
                    var propertyValue = property.GetValue(authenticationRequest.request);
                    if (propertyValue != null)
                    {
                        var bodyContentAttribute = property.GetCustomAttribute<BodyContent>();
                        if (bodyContentAttribute == null)
                        {
                            rhymbaTokenUrl = ODataUrl.AddQueryParam(rhymbaTokenUrl, property.Name, propertyValue);
                        }
                    }
                }

                foreach (var field in authenticationRequest.request.GetType().GetFields())
                {
                    var fieldValue = field.GetValue(authenticationRequest.request);
                    if (fieldValue != null)
                    {
                        rhymbaTokenUrl = ODataUrl.AddQueryParam(rhymbaTokenUrl, field.Name, fieldValue);
                    }
                }
            }

            await using var responseStream = await httpClient.GetStreamAsync(rhymbaTokenUrl);

            return await JsonSerializer.DeserializeAsync<AuthenticationResponse>(responseStream);
        }
    }
}
