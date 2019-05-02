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
using AgribattleArena.BackendServer.Services;
using Microsoft.Extensions.Options;

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
        ConstantsConfig _constants;

        public StoreController(IProfilesService profilesService, IStoreRepository storeRepository, ILogger<StoreController> logger,
            INativesRepository nativesRepository, IOptions<ConstantsConfig> constants)
        {
            _nativesRepository = nativesRepository;
            _storeRepository = storeRepository;
            _profilesService = profilesService;
            _logger = logger;
            _constants = constants.Value;
        }

        [HttpGet("actors", Name = "GetActorOffers")]
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

        [HttpPost("actors")]
        public async Task<IActionResult> BuyActor([FromBody]ActorToBuyDto actor)
        {
            //TODO Lock
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var profile = await _profilesService.GetProfileWithInfo(User);
            if(profile.Actors.Where(x => x.DeletedDate == null).Count() >= profile.BarracksSize)
            {
                ModelState.AddModelError("ProfileError", "Too many actors");
                return BadRequest(ModelState);
            }
            ActorBoughtDto newActor = await _storeRepository.BuyActor(profile.Id, profile.Resources, actor.OfferId, actor.ActorId);
            if (newActor.Error != null)
            {
                ModelState.AddModelError("StoreError", newActor.Error);
                return BadRequest(ModelState);
            }
            await _profilesService.ChangeResourcesAmount(profile, -newActor.Actor.Cost, null, null);
            _logger.Log(LogLevel.Information, "Transaction completed");
            var actorToAdd = new Models.Profile.ActorDto()
            {
                Name = actor.Name,
                ActionPointsIncome = newActor.Actor.ActionPointsIncome,
                ActorNative = newActor.Actor.ActorNative,
                AttackingSkillNative = newActor.Actor.AttackingSkillNative,
                Constitution = newActor.Actor.Constitution,
                InParty = false,
                Skills = newActor.Actor.Skills,
                Speed = newActor.Actor.Speed,
                Strength = newActor.Actor.Strength,
                Willpower = newActor.Actor.Willpower
            };
            var addedActor = await _profilesService.AddActor(profile, actorToAdd);
            if (addedActor == null)
            {
                await _profilesService.ChangeResourcesAmount(profile, newActor.Actor.Cost, null, null);
                await _storeRepository.DeclineTransaction(actor.OfferId);
                ModelState.AddModelError("ProfileError", "Unexpected error");
                return BadRequest(ModelState);
            }
            await _storeRepository.AcceptTransaction(profile.Id, actor.ActorId, newActor.Actor.Cost);
            return CreatedAtRoute("GetProfileActor", new { id = addedActor.Id }, addedActor);
        }
    }
}
