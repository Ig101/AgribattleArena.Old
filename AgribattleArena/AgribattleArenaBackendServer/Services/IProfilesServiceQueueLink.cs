using AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities;
using AgribattleArenaBackendServer.Models.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public interface IProfilesServiceQueueLink
    {
        Task<List<GenerationPartyActor>> GetAllPartyActors(List<BattleUserDto> users);
    }
}
