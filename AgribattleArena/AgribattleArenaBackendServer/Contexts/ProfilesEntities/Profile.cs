using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Contexts.ProfilesEntities
{
    public class Profile: IdentityUser
    {
        public List<Party> Parties { get; set; }
    }
}
