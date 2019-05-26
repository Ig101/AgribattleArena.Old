using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Profile
{
    public class ProfileCredentialsDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public int DonationResources { get; set; }
    }
}
