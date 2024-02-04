namespace Rhymba.Services.Search
{
    using Rhymba.Models.Common;
    using Rhymba.Models.Search;
    using Rhymba.Services.Common;
    using System.Text.Json;

    public abstract class SearchServiceWorker : AuthenticatedServiceWorker
    {
        protected SearchServiceWorker(string rhymbaAccessToken) : base("https://search.mcnemanager.com/v4/odata", rhymbaAccessToken, string.Empty)
        {

        }

        protected async Task<SearchResponse<T>?> Search<T>(SearchRequest request, string collection)
        {
            var url = $"{this.rhymbaUrl}/{collection}({(request.id != null ? request.id.ToString() : string.Empty)})/?$format=json";

            if (request.id == null || request.id == 0)
            {
                if (request.filter != null && !string.IsNullOrWhiteSpace(request.filter))
                {
                    url = ODataUrl.AddQueryParam(url, "$filter", request.filter);
                }

                if (request.id_cdl != null && request.id_cdl.Length > 0)
                {
                    url = ODataUrl.AddQueryParam(url, "$id_cdl", request.id_cdl.Select(x => x.ToString()));
                }

                if (request.skip != null)
                {
                    url = ODataUrl.AddQueryParam(url, "$skip", request.skip);
                }

                if (request.top != null)
                {
                    url = ODataUrl.AddQueryParam(url, "$top", request.top);
                }
            }

            if (request.expand != null && request.expand.Length > 0)
            {
                url = ODataUrl.AddQueryParam(url, "$expand", request.expand);
            }

            if (request.inlinecount)
            {
                url = ODataUrl.AddQueryParam(url, "$inlinecount", "allpages");
            }

            if (!string.IsNullOrWhiteSpace(request.keyword))
            {
                url = ODataUrl.AddQueryParam(url, "keyword", request.keyword);
            }

            if (request.select != null && request.select.Length > 0)
            {
                url = ODataUrl.AddQueryParam(url, "$select", request.select);
            }

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "rhymba_net_sdk");
            httpClient.DefaultRequestHeaders.Add("access_token", this.rhymbaAccessToken);
            httpClient.DefaultRequestHeaders.Add("full_search", request.full_search.ToString());

            await using var responseStream = await httpClient.GetStreamAsync(url);

            var odataValue = await JsonSerializer.DeserializeAsync<ODataValueWrapper<T[]>>(responseStream, new JsonSerializerOptions() { NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString | System.Text.Json.Serialization.JsonNumberHandling.WriteAsString });
            if (odataValue != null)
            {
                return new SearchResponse<T>()
                {
                    results = odataValue.value
                };
            }

            return new SearchResponse<T>()
            {
                results = Array.Empty<T>()
            };
        }
    }
}
