using AgribattleArena.DBProvider.Contexts.ProfileEntities;
using AgribattleArena.BackendServer.Models.Profile;
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
    [Route("api/profile")]
    public class ProfilesController: ControllerBase
    {
        IProfilesService _profilesService;
        ILogger<ProfilesController> _logger;

        public ProfilesController(IProfilesService profilesService, ILogger<ProfilesController> logger)
        {
            _profilesService = profilesService;
            _logger = logger;
        }

        [HttpGet(Name = "GetProfile")]
        public async Task<IActionResult> GetProfileWithActors()
        {
            var user = (await _profilesService.GetProfile(User, true));
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpGet("actors/{id}")]
        public async Task<IActionResult> GetProfileActor(long id)
        {
            var actor = await _profilesService.GetActor(User, id);
            if(actor!=null)
            {
                return Ok(actor);
            }
            return NotFound();
        }

        [HttpDelete("actors/{id}")]
        public async Task<IActionResult> DeleteProfileActor(long id)
        {
            if(await _profilesService.DeleteActor(User, id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
