using AgribattleArena.BackendServer.Models.Battle;
using AgribattleArena.BackendServer.Services.Interfaces;
using AgribattleArena.DBProvider.Contexts.ProfileEntities;
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
    [Route("api/queue")]
    public class QueueController: ControllerBase
    {
        IBattleService _battleService;
        IProfilesService _profileService;
        ILogger<QueueController> _logger;

        public QueueController(IBattleService battleService, IProfilesService profileService, 
            ILogger<QueueController> logger)
        {
            _battleService = battleService;
            _profileService = profileService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Enqueue([FromBody]ProfileToEnqueueDto mode)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Profile profile = await _profileService.GetProfile(User);
            if (_battleService.Enqueue(new ProfileToEnqueueEnrichedDto()
            {
                Mode = mode.Mode,
                ProfileId = profile.Id,
                RevelationLevel = _profileService.GetRevelationLevel(profile.Revelations)
            }))
            {
                return NoContent(); 
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Dequeue()
        {
            string profileId = _profileService.GetUserID(User);
            if (_battleService.Dequeue(profileId))
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
