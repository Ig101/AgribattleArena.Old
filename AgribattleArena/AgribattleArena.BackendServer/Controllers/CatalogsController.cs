using AgribattleArena.BackendServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Controllers
{
    [Route("api/catalogs")]
    public class CatalogsController: ControllerBase
    {
        INativesRepository _nativesRepository;
        IProfilesService _profilesService;
        ILogger<CatalogsController> _logger;

        public CatalogsController(INativesRepository nativesRepository, IProfilesService profilesService, ILogger<CatalogsController> logger)
        {
            _nativesRepository = nativesRepository;
            _profilesService = profilesService;
            _logger = logger;
        }

        [HttpGet("frontendNatives")]
        public async Task<IActionResult> GetFrontendNatives()
        {
            return Ok(await _nativesRepository.GetFrontendNatives());
        }

        [HttpGet("revelationLevels")]
        public IActionResult GetRevelationLevels()
        {
            return Ok(_profilesService.GetRevelationLevelsList());
        }
    }
}
