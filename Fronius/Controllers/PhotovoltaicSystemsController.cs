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
        public async Task<Paged<PhotovoltaicSystem>> Get([FromQuery]Pager pager) {
            logger.LogInformation($"GET PhotovoltaicSystems - page: {pager.PageNumber}, limit: {pager.PageSize}, sort order: {pager.SortOrder}");
            var systems = await photovoltaicService.GetSystems(pager);

            return systems;
        }

        [HttpGet("{id:guid}")]
        public async Task<PhotovoltaicSystemDetail> GetById(Guid id) {
            logger.LogInformation($"GET PhotovoltaicSystemDetail - id: {id}");

            return await photovoltaicService.GetSystemDetailAsync(id);
        }
    }
}