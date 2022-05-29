using Fronius.Models;

namespace Fronius.Services {
    public interface IPhotovoltaicService {
        Task<Paged<PhotovoltaicSystem>> GetSystemsAsync(Pager pager);

        Task<PhotovoltaicSystemDetail> GetSystemDetailAsync(Guid systemId);
    }
}