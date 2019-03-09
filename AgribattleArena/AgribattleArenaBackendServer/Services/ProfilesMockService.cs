using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities;
using AgribattleArenaBackendServer.Models.Profiles;

namespace AgribattleArenaBackendServer.Services
{
    public class ProfilesMockService : IProfilesServiceSceneLink
    {
        public List<PlayerActor> GetPlayerActors(int playerId)
        {
            throw new NotImplementedException();
        }
    }
}
