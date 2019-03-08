using AgribattleArenaBackendServer.Engine;
using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Engine.NativeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public interface IEngineService
    {
        void AddNewScene(int id, List<int> players, IProfilesServiceSceneLink profilesService, int seed);
        void ReinitializeNatives(INativesServiceSceneLink nativesService);
    }
}
