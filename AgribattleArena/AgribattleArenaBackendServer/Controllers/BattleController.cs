using AgribattleArenaBackendServer.Contexts.ProfilesEntities;
using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities;
using AgribattleArenaBackendServer.Engine.NativeManager;
using AgribattleArenaBackendServer.Hubs;
using AgribattleArenaBackendServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Controllers
{
    [Authorize]
    [Route("api/battle")]
    public class BattleController: ControllerBase
    {
        IProfilesService profilesService;
        IBattleService battleService;
        INativesServiceSceneLink nativesService;
        IEngineService engineService;
        IQueueingService queueingService;

        public BattleController(IBattleService battleService, INativesServiceSceneLink nativesService, 
            IEngineService engineService, IQueueingService queueingService, IProfilesService profilesService)
        {
            this.profilesService = profilesService;
            this.battleService = battleService;
            this.queueingService = queueingService;
            this.engineService = engineService;
            this.nativesService = nativesService;
        }

        [HttpPost]
        public async Task<IActionResult> EnqueueNewPlayer (int partyId)
        {
            string userId = (await profilesService.GetProfile(User)).Id;

            return NoContent();
        }
    }
}
