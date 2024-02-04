namespace Rhymba.Services.Common
{
    using Rhymba.Models.Authentication;
    using Rhymba.Models.Playlist;
    using System.Security.Cryptography;
    using System.Text;

    public abstract class ServiceWorkerBase
    {
        protected readonly string rhymbaUrl;

        protected ServiceWorkerBase(string rhymbaUrl)
        {
            this.rhymbaUrl = rhymbaUrl;
        }
    }

    public abstract class AuthenticatedServiceWorker : ServiceWorkerBase
    {
        protected readonly string rhymbaAccessToken;
        protected readonly string rhymbaAccessSecret;

        protected AuthenticatedServiceWorker(string rhymbaUrl, string rhymbaAccessToken, string rhymbaAccessSecret) : base(rhymbaUrl)
        {
            this.rhymbaAccessToken = rhymbaAccessToken;
            this.rhymbaAccessSecret = rhymbaAccessSecret;
        }

        protected async Task<AuthenticationResponse?> CreateAuthenticationToken<T>(T request, string method)
        {
            return await Authentication.CreateContentToken(this.rhymbaUrl, new AuthenticationRequest<T>()
            {
                access_secret = this.rhymbaAccessSecret,
                access_token = this.rhymbaAccessToken,
                method = method,
                request = request,
                ttl = 86400
            });
        }
    }

    public abstract class HashAuthenticatedServiceWorker : ServiceWorkerBase
    {
        protected readonly string rhymbaAccessToken;
        protected readonly string rhymbaAccessSecret;
        protected readonly string playlistPublicKey;
        protected readonly string playlistPrivateKey;

        protected HashAuthenticatedServiceWorker(string rhymbaUrl, string rhymbaAccessToken, string rhymbaAccessSecret, string playlistPublicKey, string playlistPrivateKey) : base(rhymbaUrl)
        {
            this.rhymbaAccessToken = rhymbaAccessToken;
            this.rhymbaAccessSecret = rhymbaAccessSecret;
            this.playlistPublicKey = playlistPublicKey;
            this.playlistPrivateKey = playlistPrivateKey;
        }

        protected string CreateAuthenticationHash<TRequest>(TRequest request, string method, string route)
        {
            var keyBytes = Encoding.UTF8.GetBytes(this.playlistPrivateKey);
            var requestData = $"{method.ToUpper()}&{route}";

            if (request != null)
            {
                foreach (var property in request.GetType().GetProperties())
                {
                    var propertyValue = property.GetValue(request);
                    if (propertyValue != null)
                    {
                        // {System.Net.WebUtility.UrlEncode(propertyValue)}
                        if (propertyValue is string)
                        {
                            requestData += $"&{property.Name}={System.Net.WebUtility.UrlEncode((string)propertyValue)}";
                        }
                        else
                        {
                            requestData += $"&{property.Name}={propertyValue}";
                        }
                    }
                }

                foreach (var field in request.GetType().GetFields())
                {
                    var fieldValue = field.GetValue(request);
                    if (fieldValue != null)
                    {
                        requestData += $"&{field.Name}={fieldValue}";
                    }
                }
            }
            var dataBytes = Encoding.UTF8.GetBytes(requestData);
            var hmac = new HMACSHA256(keyBytes);
            var hmacBytes = hmac.ComputeHash(dataBytes);

            return Convert.ToBase64String(hmacBytes);
        }
    }
} 
