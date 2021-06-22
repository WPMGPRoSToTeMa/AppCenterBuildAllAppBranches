using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppCenterBuildAllAppBranches
{
    sealed class AppCenterRestApiClient : IDisposable
    {
        private const string apiUri = "https://api.appcenter.ms/v0.1/";

        private readonly HttpClient httpClient = new();
        private readonly string urlEncodedOwnerName;
        private readonly string urlEncodedAppName;
        private readonly JsonSerializerOptions jsonOptions;

        // TODO: probably ownerName and appName should be passed to every method instead of stored in the object
        // TODO: retry logic in case of transient failures
        public AppCenterRestApiClient(string ownerName, string appName, string apiKey)
        {
            this.urlEncodedOwnerName = WebUtility.UrlEncode(ownerName);
            this.urlEncodedAppName = WebUtility.UrlEncode(appName);

            this.httpClient.BaseAddress = new Uri(apiUri);
            this.httpClient.DefaultRequestHeaders.Add("X-API-Token", apiKey);
            
            this.jsonOptions = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        // https://openapi.appcenter.ms/#/build/builds_listBranches
        public async Task<AppBranch[]> GetAppBranches()
        {
            return await this.httpClient.GetFromJsonAsync<AppBranch[]>($"apps/{this.urlEncodedOwnerName}/{this.urlEncodedAppName}/branches", this.jsonOptions);
        }

        // https://openapi.appcenter.ms/#/build/builds_create
        public async Task<Build> CreateBuild(string branchName, BuildParams buildParams)
        {
            HttpResponseMessage response = await this.httpClient.PostAsJsonAsync(
                $"apps/{this.urlEncodedOwnerName}/{this.urlEncodedAppName}/branches/{WebUtility.UrlEncode(branchName)}/builds", buildParams, this.jsonOptions);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Build>(this.jsonOptions);
        }

        // https://openapi.appcenter.ms/#/build/builds_get
        public async Task<Build> GetBuild(long id)
        {
            return await this.httpClient.GetFromJsonAsync<Build>($"apps/{this.urlEncodedOwnerName}/{this.urlEncodedAppName}/builds/{id}", this.jsonOptions);
        }

        // https://openapi.appcenter.ms/#/build/builds_getDownloadUri
        public async Task<Download> GetDownload(long buildId, DownloadType type)
        {
            return await this.httpClient.GetFromJsonAsync<Download>(
                $"apps/{this.urlEncodedOwnerName}/{this.urlEncodedAppName}/builds/{buildId}/downloads/{type.ToString().ToLower()}", this.jsonOptions);
        }

        public void Dispose()
        {
            this.httpClient.Dispose();
        }
    }
}
