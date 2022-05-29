using Fronius.Models;
using Fronius.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fronius.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class PhotovoltaicSystemsController : ControllerBase {
        private readonly IPhotovoltaicService photovoltaicService;
        private readonly ILogger<PhotovoltaicSystemsController> logger;

        public PhotovoltaicSystemsController(IPhotovoltaicService photovoltaicService,
            ILogger<PhotovoltaicSystemsController> logger) {
            this.photovoltaicService = photovoltaicService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<PhotovoltaicSystem>> Get(int start = 0, int limit = 5) {
            logger.LogInformation($"GET PhotovoltaicSystems - start: {start}, limit: {limit}");
            var systems = await photovoltaicService.GetSystems(start, limit);
    var system = await photovoltaicService.GetSystemDetailAsync(systems.First().Id);
            return systems;
        }

     
    }
}