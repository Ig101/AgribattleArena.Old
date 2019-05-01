using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgribattleArena.ConfigurationServer.Services
{
    interface IProfilesService
    {
        Task<bool> IsAdmin(ClaimsPrincipal user);
    }
}
