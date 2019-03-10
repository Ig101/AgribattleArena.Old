using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities;
using AgribattleArenaBackendServer.Models.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public interface IEngineServiceQueueLink
    {
        bool AddNewScene(int id, List<BattleUserDto> players, List<GenerationPartyActor> playerActors, ILevelGenerator levelGenerator, int seed);
        int GetNextRandomNumber();
    }
}
