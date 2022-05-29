using Fronius.Models;

namespace Fronius.Services {
    public interface IPhotovoltaicService {
        Task<Paged<PhotovoltaicSystem>> GetSystems(Pager pager);

        Task<PhotovoltaicSystemDetail> GetSystemDetailAsync(Guid systemId);
    }
}