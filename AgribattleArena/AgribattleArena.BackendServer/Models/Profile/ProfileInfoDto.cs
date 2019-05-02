using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Profile
{
    public class ProfileInfoDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Resources { get; set; }
        public int DonationResources { get; set; }
        public int Revelations { get; set; }
        public int BarracksSize { get; set; }
    }
}
