using AgribattleArena.DBProvider.Contexts;
using AgribattleArena.DBProvider.Contexts.ProfileEntities;
using AgribattleArena.BackendServer.Models.Profile;
using AgribattleArena.BackendServer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AgribattleArena.BackendServer.Services
{
    public class ProfilesService: UserManager<Profile>, IProfilesService, IProfilesServiceAggregated
    {
        IStoredInfoService _storedInfoService;
        ProfilesContext _context;
        ConstantsConfig _constants;

        public ProfilesService(IUserStore<Profile> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<Profile> passwordHasher,
            IEnumerable<IUserValidator<Profile>> userValidators, IEnumerable<IPasswordValidator<Profile>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<Profile>> logger, IStoredInfoService storedInfoService,
            ProfilesContext context, IOptions<ConstantsConfig> constants)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _storedInfoService = storedInfoService;
            _context = context;
            _constants = constants.Value;
        }

        public async Task<Profile> GetProfile(ClaimsPrincipal user)
        {
            Profile profile = await GetUserAsync(user);
            return profile;
        }

        public async Task<Profile> GetProfileWithInfo (ClaimsPrincipal user)
        {
            Profile profile = await _context.Users
                .Include(x => x.Actors)
                .ThenInclude(x => x.Skills)
                .FirstOrDefaultAsync(x => x.UserName == user.Identity.Name);
            return profile;
        }

        public ActorDto GetActor(Profile profile, long actorId)
        {
            return AutoMapper.Mapper.Map<ActorDto>(profile.Actors?.Find(x => x.Id == actorId && x.DeletedDate==null));
        }

        public async Task<ActorDto> GetActor(ClaimsPrincipal user, long actorId)
        {
            Profile profile = await GetProfileWithInfo(user);
            return GetActor(profile, actorId);
        }

        public async Task<bool> DeleteActor(Profile profile, long actorId)
        {
            Actor actorToDelete = profile.Actors?.Find(x => x.Id == actorId && x.DeletedDate == null);
            if (actorToDelete != null)
            {
                actorToDelete.DeletedDate = DateTime.Now;
                var result = await UpdateAsync(profile);
                if (result.Succeeded)
                    return true;
            }
            return false;
        }

        public async Task<bool> DeleteActor(ClaimsPrincipal user, long actorId)
        {
            Profile profile = await GetProfileWithInfo(user);
            return await DeleteActor(profile, actorId);
        }

        public async Task<ActorDto> AddActor(Profile profile, ActorDto actor)
        {
            if (profile.Actors.Count >= _constants.ProfileActorsLimit) return null;
            Actor newActor = AutoMapper.Mapper.Map<Actor>(actor);
            profile.Actors.Add(newActor);
            await UpdateAsync(profile);
            return AutoMapper.Mapper.Map<ActorDto>(newActor);
        }

        public async Task<ActorDto> AddActor(ClaimsPrincipal user, ActorDto actor)
        {
            Profile profile = await GetProfileWithInfo(user);
            return await AddActor(profile, actor);
        }

        public string GetUserID(ClaimsPrincipal user)
        {
            return GetUserId(user);
        }

        public async Task<ProfileDto> UpdateProfile(ClaimsPrincipal user, ProfileUpdateDto changing)
        {
            Profile profile = await GetUserAsync(user);
            if (changing.Email != null && changing.EmailToken!=null)
            {
                await ChangeEmailAsync(profile, changing.Email, changing.EmailToken);
            }
            if (changing.Password!=null && changing.OldPassword != null)
            {
                await ChangePasswordAsync(profile, changing.OldPassword, changing.Password);
            }
            return AutoMapper.Mapper.Map<ProfileDto>(profile);
        }

        public async Task<ProfileDto> ChangeResourcesAmount (Profile profile, int? resources, int? donationResources, int? revelations)
        {
            if (resources != null) profile.Resources += resources.Value;
            if (donationResources != null) profile.DonationResources += resources.Value;
            if (revelations != null)
            {
                profile.Revelations += revelations.Value;
                _storedInfoService.RevelationsMemory += revelations.Value;
            }
            var result = await UpdateAsync(profile);
            if (result.Succeeded)
                return AutoMapper.Mapper.Map<ProfileDto>(profile);
            return null;
        }

        public async Task<ProfileDto> ChangeResourcesAmount(ClaimsPrincipal user, int? resources, int? donationResources, int? revelations)
        {
            Profile profile = await GetUserAsync(user);
            return await ChangeResourcesAmount(profile, resources, donationResources, revelations);
        }

        public IEnumerable<ProfileInfoDto> GetAllProfiles()
        {
            return AutoMapper.Mapper.Map<IEnumerable<ProfileInfoDto>>(_context.Users);
        }

        public async Task<bool> IsAdmin(ClaimsPrincipal user)
        {
            Profile profile = await GetUserAsync(user);
            var roles = await GetRolesAsync(profile);
            return roles.Contains("admin");
        }
    }
}
