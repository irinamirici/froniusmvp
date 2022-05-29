using Fronius.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fronius.Services {
    public class PhotovoltaicService : IPhotovoltaicService {
        private const string PvSystemsCacheKey = "pvsystems";

        private readonly int cacheInSeconds;
        private readonly IMemoryCache memoryCache;
        private readonly IHttpClientFactory httpClientFactory;

        public PhotovoltaicService(IMemoryCache memoryCache,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration) {
            this.memoryCache = memoryCache;
            this.httpClientFactory = httpClientFactory;
            this.cacheInSeconds = int.Parse(configuration["ApiCacheInSeconds"]);
        }

        public async Task<PhotovoltaicSystemDetail> GetSystemDetailAsync(Guid systemId) {
            var dtos = await OnGetCacheGetOrCreateAsync();
            var systemDto = dtos.FirstOrDefault(x => x.Id == systemId);
            if (systemDto == null) {
                throw new InvalidOperationException($"System {systemId} not found. Please refresh page.");
            }

            var systemMessages = await GetServicestemMessagesAsync(systemId);

            return new PhotovoltaicSystemDetail(systemDto, systemMessages);
        }

        private async Task<IReadOnlyCollection<Dtos.ServiceMessageDto>> GetServicestemMessagesAsync(Guid systemId) {
            var httpClient = httpClientFactory.CreateClient(Constants.FroniusApiClient);
            var httpResponseMessage = await httpClient.GetAsync($"loadservicemessages?pvId={systemId}");

            if (httpResponseMessage.IsSuccessStatusCode) {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                return await JsonSerializer.DeserializeAsync<List<Dtos.ServiceMessageDto>>(contentStream, GetOptions());
            }

            throw new InvalidOperationException($"Could not read service message for {systemId}");
        }

        public async Task<Paged<PhotovoltaicSystem>> GetSystems(Pager pager) {
            var dtos = await OnGetCacheGetOrCreateAsync();

            var models = dtos.Skip(pager.Skip)
                .Take(pager.PageSize)
                .Select(x => new PhotovoltaicSystem(x))
                .ToList();

            return new Paged<PhotovoltaicSystem>(models, dtos.Count);
        }

        private async Task<IReadOnlyCollection<Dtos.PhotovoltaicSystemDetailDto>> OnGetCacheGetOrCreateAsync() {
            return await memoryCache.GetOrCreateAsync(
                PvSystemsCacheKey,
                cacheEntry => {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheInSeconds);
                    return GetPvSystems();
                });

        }

        private async Task<IReadOnlyCollection<Dtos.PhotovoltaicSystemDetailDto>> GetPvSystems() {
            var httpClient = httpClientFactory.CreateClient(Constants.FroniusApiClient);
            var httpResponseMessage = await httpClient.GetAsync("loadpvsystems");

            if (httpResponseMessage.IsSuccessStatusCode) {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                return await JsonSerializer.DeserializeAsync<List<Dtos.PhotovoltaicSystemDetailDto>>(contentStream, GetOptions());
            }

            throw new InvalidOperationException("Could not read PvSystems data");
        }

        private static JsonSerializerOptions GetOptions() {
            return new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
        }
    }
}
