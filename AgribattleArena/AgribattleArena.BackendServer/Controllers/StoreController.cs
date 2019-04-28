using AgribattleArena.BackendServer.Contexts.StoreEntities;
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
        IStoreRepository _storeRepository;
        ILogger<StoreController> _logger;

        public StoreController(IProfilesService profilesService, IStoreRepository storeRepository, ILogger<StoreController> logger)
        {
            _storeRepository = storeRepository;
            _profilesService = profilesService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetStoreInfo ()
        {
            return Ok(new StoreInfoDto
            {
                ActorOfferLink = Url.RouteUrl("GetActorOffers")
            });
        }

        [HttpGet("actors", Name = "GetActorOffers")]
        public async Task<IActionResult> GetActorOffers ()
        {
            string userId = _profilesService.GetUserID(User);
            Offer offer = await _storeRepository.GetOffer(userId);
            if(offer!=null)
            {
                return Ok(AutoMapper.Mapper.Map<OfferDto>(offer));
            }
            return StatusCode(500);
        }
    }
}
