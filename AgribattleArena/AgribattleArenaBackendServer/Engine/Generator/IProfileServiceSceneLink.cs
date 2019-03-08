using AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Generator
{
    public interface IProfilesServiceSceneLink
    {
        List<PlayerActor> GetPlayerActors(int playerId);
    }
}
