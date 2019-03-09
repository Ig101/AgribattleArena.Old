using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Engine.NativeManager;
using AgribattleArenaBackendServer.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Controllers
{
    public class BattleController: ControllerBase
    {
        IBattlefieldService battleService;
        INativesServiceSceneLink nativesService;
        IProfilesServiceSceneLink profilesService;
        IEngineService engineService;
        IQueueingService queueingService;

        public BattleController(IBattlefieldService battleService, INativesServiceSceneLink nativesService, 
            IProfilesServiceSceneLink profilesService, IEngineService engineService, IQueueingService queueingService)
        {
            this.queueingService = queueingService;
            this.engineService = engineService;
            this.profilesService = profilesService;
            this.nativesService = nativesService;
            this.battleService = battleService;
        }

        public void StartNewBattle ()
        {

        }
    }
}
