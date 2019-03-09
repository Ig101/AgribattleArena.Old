using AgribattleArenaBackendServer.Models.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public interface IProfilesService
    {
        List<RoleDto> GetRoles();
        List<RightDto> GetRights();

        PlayerActorDto GetPlayerActor(int actorId);
        PlayerDto GetPlayer(int playerId);
        ProfileDto GetProfile(string login);
        RoleDto GetRole(int roleId);
    }
}
