using AgribattleArenaBackendServer.Contexts;
using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public class ProfilesService : IProfilesService, IProfilesServiceSceneLink
    {
        ProfilesContext context;

        public ProfilesService (ProfilesContext context)
        {
            this.context = context;
        }

        public List<PlayerActor> GetPlayerActors(int playerId)
        {
            throw new NotImplementedException();
        }
    }
}
