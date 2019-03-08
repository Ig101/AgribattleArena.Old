using AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities;
using AgribattleArenaBackendServer.Engine.Synchronization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Generator
{
    public interface ILevelGenerator
    {
        GenerationSet GenerateNewScene(IProfilesServiceSceneLink profilesService, List<int> playerIds, int seed);
    }
}
