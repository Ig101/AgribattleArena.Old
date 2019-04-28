using AgribattleArena.BackendServer.Contexts.ProfileEntities;
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
        Task<Profile> GetProfile(ClaimsPrincipal user, bool withActors);
        Task<Actor> GetActor(ClaimsPrincipal user, long actorId);
        Task<bool> DeleteActor(ClaimsPrincipal user, long actorId);
        Task<Profile> UpdateProfile(ClaimsPrincipal user, ProfileUpdateDto changing);
    }
}
