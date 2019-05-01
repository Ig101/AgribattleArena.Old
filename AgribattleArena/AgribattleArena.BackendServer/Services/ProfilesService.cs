using AgribattleArena.BackendServer.Contexts;
using AgribattleArena.BackendServer.Contexts.ProfileEntities;
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

namespace AgribattleArena.BackendServer.Services
{
    public class ProfilesService: UserManager<Profile>, IProfilesService, IProfilesServiceAggregated
    {
        IStoredInfoService _storedInfoService;
        ProfilesContext _context;

        public ProfilesService(IUserStore<Profile> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<Profile> passwordHasher,
            IEnumerable<IUserValidator<Profile>> userValidators, IEnumerable<IPasswordValidator<Profile>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<Profile>> logger, IStoredInfoService storedInfoService,
            ProfilesContext context)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _storedInfoService = storedInfoService;
            _context = context;
        }

        public async Task<Profile> GetProfile(ClaimsPrincipal user, bool withActors)
        {
            Profile profile = await GetUserAsync(user);
            if (withActors)
            {
                profile.Actors?.RemoveAll(x => x.DeletedDate != null);
            }
            return profile;
        }

        public async Task<Actor> GetActor(ClaimsPrincipal user, long actorId)
        {
            Profile profile = await GetUserAsync(user);
            return profile.Actors?.Find(x => x.Id == actorId && x.DeletedDate == null);
        }

        public async Task<bool> DeleteActor(ClaimsPrincipal user, long actorId)
        {
            Profile profile = await GetUserAsync(user);
            Actor actorToDelete = profile.Actors?.Find(x => x.Id == actorId && x.DeletedDate == null);
            if (actorToDelete != null)
            {
                actorToDelete.DeletedDate = DateTime.Now;
                var result = await UpdateAsync(profile);
                if(result.Succeeded)
                    return true;
            }
            return false;
        }

        public string GetUserID(ClaimsPrincipal user)
        {
            return GetUserId(user);
        }

        public async Task<Profile> UpdateProfile(ClaimsPrincipal user, ProfileUpdateDto changing)
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
            return profile;
        }

        public async Task<Profile> ChangeResourcesAmount (ClaimsPrincipal user, int? resources, int? donationResources, int? revelations)
        {
            Profile profile = await GetUserAsync(user);
            if (resources != null) profile.Resources += resources.Value;
            if (donationResources != null) profile.DonationResources += resources.Value;
            if (revelations != null)
            {
                profile.Revelations += revelations.Value;
                _storedInfoService.RevelationsMemory += revelations.Value;
            }
            var result = await UpdateAsync(profile);
            if (result.Succeeded)
                return profile;
            return null;
        }

        public IEnumerable<ProfileDto> GetAllProfiles()
        {
            return AutoMapper.Mapper.Map<IEnumerable<ProfileDto>>(_context.Users);
        }
    }
}
