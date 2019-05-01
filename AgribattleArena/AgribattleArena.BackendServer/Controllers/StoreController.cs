using AgribattleArena.DBProvider.Contexts.StoreEntities;
using AgribattleArena.BackendServer.Models.Store;
using AgribattleArena.BackendServer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Controllers
{
    [Authorize]
    [Route("api/store")]
    public class StoreController: ControllerBase
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

        [HttpGet("actors/offer", Name = "GetActorOffers")]
        public async Task<IActionResult> GetActorOffers ()
        {
            string userId = _profilesService.GetUserID(User);
            var offer = await _storeRepository.GetOffer(userId);
            if(offer!=null)
            {
                return Ok(offer);
            }
            return StatusCode(500);
        }
    }
}
