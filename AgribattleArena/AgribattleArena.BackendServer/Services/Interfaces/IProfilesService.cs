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
        int GetRevelationLevel(int revelations);
        Task<bool> IsAdmin(ClaimsPrincipal user);
        Task<Profile> GetProfile(ClaimsPrincipal user);
        Task<Profile> GetProfileWithInfo(ClaimsPrincipal user);
        Task<Profile> GetProfileWithInfo(string userId);
        Task<ActorDto> GetActor(ClaimsPrincipal user, long actorId);
        Task<bool> DeleteActor(ClaimsPrincipal user, long actorId);
        Task<ActorDto> AddActor(ClaimsPrincipal user, ActorDto actor);
        ActorDto GetActor(Profile profile, long actorId);
        Task<bool> DeleteActor(Profile profile, long actorId);
        Task<ActorDto> AddActor(Profile profile, ActorDto actor);
        Task<ProfileDto> UpdateProfile(ClaimsPrincipal user, ProfileUpdateDto changing);
        Task<ProfileDto> ChangeResourcesAmount(ClaimsPrincipal user, int? resources, int? donationResources, int? revelations);
        Task<ProfileDto> ChangeResourcesAmount(Profile profile, int? resources, int? donationResources, int? revelations);
    }
}
