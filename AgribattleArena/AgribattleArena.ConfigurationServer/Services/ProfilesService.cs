using AgribattleArena.DBProvider.Contexts;
using AgribattleArena.DBProvider.Contexts.ProfileEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgribattleArena.ConfigurationServer.Services
{
    public class ProfilesService: UserManager<Profile>, IProfilesService
    {
        ProfilesContext _context;

        public ProfilesService(IUserStore<Profile> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<Profile> passwordHasher,
            IEnumerable<IUserValidator<Profile>> userValidators, IEnumerable<IPasswordValidator<Profile>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<Profile>> logger, ProfilesContext context)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _context = context;
        }

        public async Task<bool> IsAdmin(ClaimsPrincipal user)
        {
            Profile profile = await GetUserAsync(user);
            var roles = await GetRolesAsync(profile);
            return roles.Contains("admin");
        }
    }
}
