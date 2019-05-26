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
    public class ProfilesController : ControllerBase
    {
        IProfilesService _profilesService;
        ILogger<ProfilesController> _logger;
        IBattleService _battleService;

        public ProfilesController(IProfilesService profilesService, ILogger<ProfilesController> logger,
            IBattleService battleService)
        {
            _profilesService = profilesService;
            _logger = logger;
            _battleService = battleService;
        }

        [HttpGet(Name = "GetProfile")]
        public async Task<IActionResult> GetProfile()
        {
            var user = (await _profilesService.GetProfile(User));
            if (user != null)
            {
                ProfileInfoDto profile = AutoMapper.Mapper.Map<ProfileInfoDto>(user);
                profile.RevelationLevel = _profilesService.GetRevelationLevel(user.Revelations);
                return Ok(profile);
            }
            return NotFound();
        }

        [HttpGet("credentials", Name = "GetProfileCredentials")]
        public async Task<IActionResult> GetProfileCredentials()
        {
            var user = (await _profilesService.GetProfile(User));
            if (user != null)
            {
                ProfileCredentialsDto profile = AutoMapper.Mapper.Map<ProfileCredentialsDto>(user);
                return Ok(profile);
            }
            return NotFound();
        }

        [HttpGet("status", Name = "GetProfileStatus")]
        public IActionResult GetProfileStatus()
        {
            var userId = _profilesService.GetUserID(User);
            var info = _battleService.GetProfileBattleStatus(userId);
            return Ok(info);
        }

        [HttpGet("actors", Name = "GetProfileWithInfo")]
        public async Task<IActionResult> GetProfileWithActors()
        {
            var user = (await _profilesService.GetProfileWithInfo(User));
            user.Actors.RemoveAll(x => x.DeletedDate != null);
            if (user != null)
            {
                ProfileDto profile = AutoMapper.Mapper.Map<ProfileDto>(user);
                profile.RevelationLevel = _profilesService.GetRevelationLevel(user.Revelations);
                return Ok(profile);
            }
            return NotFound();
        }

        [HttpGet("actors/{id}", Name = "GetProfileActor")]
        public async Task<IActionResult> GetProfileActor(long id)
        {
            var actor = await _profilesService.GetActor(User, id);
            if (actor != null)
            {
                return Ok(actor);
            }
            return NotFound();
        }

        [HttpDelete("actors/{id}")]
        public async Task<IActionResult> DeleteProfileActor(long id)
        {
            if (await _profilesService.DeleteActor(User, id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
