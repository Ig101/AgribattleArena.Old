using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Profile
{
    public class ProfileWithActorsDto: ProfileDto
    {
        public IEnumerable<ActorDto> Actors { get; set; }
    }
}
