using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Profile
{
    public class ProfileDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Resources { get; set; }
        public int DonationResources { get; set; }
        public int Revelations { get; set; }
        public int BarracksSize { get; set; }
        public IEnumerable<ActorDto> Actors { get; set; }
    }
}
