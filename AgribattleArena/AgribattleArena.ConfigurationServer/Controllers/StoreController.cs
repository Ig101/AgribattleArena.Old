using AgribattleArena.ConfigurationServer.Models;
using AgribattleArena.ConfigurationServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.ConfigurationServer.Controllers
{
    [Authorize]
    [Route("api/store")]
    public class StoreController : ControllerBase
    {
        IProfilesService _profilesService;
        INativesRepository _nativesRepository;
        IStoreRepository _storeRepository;
        ILogger<StoreController> _logger;
        
        public StoreController(IProfilesService profilesService, IStoreRepository storeRepository, ILogger<StoreController> logger,
            INativesRepository nativesRepository)
        {
            _nativesRepository = nativesRepository;
            _storeRepository = storeRepository;
            _profilesService = profilesService;
            _logger = logger;
        }

        [HttpPost("actors")]
        public async Task<IActionResult> AddNewActor([FromBody]ActorToAddDto actor)
        {
            if(await _profilesService.IsAdmin(User))
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                if(await _storeRepository.AddNewActor(actor))
                {
                    return NoContent();
                }
                else
                {
                    ModelState.AddModelError("UniqueConstraintError", "Actor already exists");
                    return BadRequest(ModelState);
                }
            }
            return Forbid();
        }
    }
}
