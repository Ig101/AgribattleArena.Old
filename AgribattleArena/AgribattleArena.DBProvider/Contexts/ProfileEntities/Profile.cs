using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.DBProvider.Contexts.ProfileEntities
{
    public class Profile : IdentityUser
    {
        public int Resources { get; set; }
        public int DonationResources { get; set; }
        public int Revelations { get; set; }
        public int BarracksSize { get; set; }

        public List<Actor> Actors { get; set; }
    }
}
