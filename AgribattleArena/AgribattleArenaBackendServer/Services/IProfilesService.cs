using AgribattleArenaBackendServer.Contexts.ProfilesEntities;
using AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities;
using AgribattleArenaBackendServer.Models.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public interface IProfilesService
    {
        Task<Profile> GetProfile(ClaimsPrincipal user);
        Task<List<PartyActor>> GetPartyActors(BattleUserDto user);
        Task<List<PartyActor>> GetAllPartyActors(List<BattleUserDto> users);
    }
}
