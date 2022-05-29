using Fronius.Models;

namespace Fronius.Services {
    public interface IPhotovoltaicService {
        Task<IReadOnlyCollection<PhotovoltaicSystem>> GetSystems(int start = 0, int limit = 5);

        Task<PhotovoltaicSystemDetail> GetSystemDetailAsync(Guid systemId);
    }
}