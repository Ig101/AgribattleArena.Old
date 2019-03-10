using AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities;
using AgribattleArenaBackendServer.Engine.Synchronization;
using AgribattleArenaBackendServer.Models.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Generator
{
    public interface ILevelGenerator
    {
        GenerationSet GenerateNewScene(List<GenerationPartyActor> playerActors, List<BattleUserDto> playerIds, int seed);
    }
}
