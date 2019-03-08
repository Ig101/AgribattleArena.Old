using AgribattleArenaBackendServer.Contexts;
using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Engine.NativeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public class BattlefieldService: IBattlefieldService
    {
        INativesServiceSceneLink nativesService;
        IProfilesServiceSceneLink profilesService;
        IEngineService engineService;
        BattlefieldContext context;

        public BattlefieldService(BattlefieldContext context, IProfilesServiceSceneLink profilesService,
            INativesServiceSceneLink nativesService, IEngineService engineService)
        {
            this.context = context;
            this.profilesService = profilesService;
            this.nativesService = nativesService;
            this.engineService = engineService;
        }

        public void StartNewBattle(List<int> players)
        {
            throw new NotImplementedException();
        }
    }
}
