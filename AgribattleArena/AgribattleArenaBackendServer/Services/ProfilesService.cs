using AgribattleArenaBackendServer.Contexts.ProfilesEntities;
using AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities;
using AgribattleArenaBackendServer.Models.Battle;
using AgribattleArenaBackendServer.Models.Natives;
using AgribattleArenaBackendServer.Models.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public class ProfilesService : UserManager<Profile>, IProfilesService
    {
        public ProfilesService(IUserStore<Profile> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<Profile> passwordHasher, 
            IEnumerable<IUserValidator<Profile>> userValidators, IEnumerable<IPasswordValidator<Profile>> passwordValidators, ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<Profile>> logger) 
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public async Task<Profile> GetProfile(ClaimsPrincipal user)
        {
            return await GetUserAsync(user);
        }

        public async Task<List<PartyActor>> GetPartyActors (BattleUserDto user)
        {
            Profile profile = await FindByIdAsync(user.UserId);
            Party party = profile.Parties.Find(x => x.Id == user.PartyId);
            if (party != null)
            {
                return party.Actors.Select(x => new PartyActor(x.ActorNative, user.PartyId, AutoMapper.Mapper.Map<RoleModelNativeToAddDto>(x))).ToList();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<PartyActor>> GetAllPartyActors(List<BattleUserDto> users)
        {
            List<PartyActor> aggregatedList = new List<PartyActor>();
            foreach(BattleUserDto user in users)
            {
                aggregatedList.AddRange(await GetPartyActors(user));
            }
            return aggregatedList;
        }
    }
}
