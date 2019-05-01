using AgribattleArena.DBProvider.Contexts.ProfileEntities;
using AgribattleArena.BackendServer.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Services.Interfaces
{
    public interface IProfilesService
    {
        string GetUserID(ClaimsPrincipal user);
        Task<bool> IsAdmin(ClaimsPrincipal user);
        Task<ProfileDto> GetProfile(ClaimsPrincipal user, bool withActors);
        Task<ActorDto> GetActor(ClaimsPrincipal user, long actorId);
        Task<bool> DeleteActor(ClaimsPrincipal user, long actorId);
        Task<ProfileDto> UpdateProfile(ClaimsPrincipal user, ProfileUpdateDto changing);
    }
}
