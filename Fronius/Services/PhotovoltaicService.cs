using Fronius.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq.Dynamic.Core;

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
            cacheInSeconds = int.Parse(configuration["ApiCacheInSeconds"]);
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

        public async Task<Paged<PhotovoltaicSystem>> GetSystemsAsync(Pager pager) {
            var queriableDtos = (await OnGetCacheGetOrCreateAsync()).AsQueryable();

            if(!string.IsNullOrWhiteSpace(pager.SortBy)) {
                queriableDtos = queriableDtos.OrderBy(pager.SortCriteria);
            }

            var models = queriableDtos
                .Skip(pager.Skip)
                .Take(pager.PageSize)
                .Select(x => new PhotovoltaicSystem(x))
                .ToList();

            return new Paged<PhotovoltaicSystem>(models, queriableDtos.Count());
        }

        private async Task<IReadOnlyCollection<Dtos.PhotovoltaicSystemDetailDto>> OnGetCacheGetOrCreateAsync() {
            return await memoryCache.GetOrCreateAsync(
                PvSystemsCacheKey,
                cacheEntry => {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheInSeconds);
                    return GetPvSystemsAsync();
                });

        }

        private async Task<IReadOnlyCollection<Dtos.PhotovoltaicSystemDetailDto>> GetPvSystemsAsync() {
            var httpClient = httpClientFactory.CreateClient(Constants.FroniusApiClient);
            var httpResponseMessage = await httpClient.GetAsync("loadpvsystems");

            if (!httpResponseMessage.IsSuccessStatusCode) {
                throw new InvalidOperationException("Could not read PvSystems data");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<List<Dtos.PhotovoltaicSystemDetailDto>>(contentStream, GetOptions());
        }

        private async Task<IReadOnlyCollection<Dtos.ServiceMessageDto>> GetServicestemMessagesAsync(Guid systemId) {
            var httpClient = httpClientFactory.CreateClient(Constants.FroniusApiClient);
            var httpResponseMessage = await httpClient.GetAsync($"loadservicemessages?pvId={systemId}");

            if (!httpResponseMessage.IsSuccessStatusCode) {
                throw new InvalidOperationException($"Could not read service message for {systemId}");
            }
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<List<Dtos.ServiceMessageDto>>(contentStream, GetOptions());
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
