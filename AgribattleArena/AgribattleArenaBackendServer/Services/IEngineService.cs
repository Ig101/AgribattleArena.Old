using AgribattleArenaBackendServer.Engine;
using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities;
using AgribattleArenaBackendServer.Engine.NativeManager;
using AgribattleArenaBackendServer.Models.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public interface IEngineService
    {
        int GetNextRandomNumber();
        bool AddNewScene(int id, List<BattleUserDto> players, List<PartyActor> playerActors, int seed);
        void ReinitializeNatives(INativesServiceSceneLink nativesService);
    }
}
