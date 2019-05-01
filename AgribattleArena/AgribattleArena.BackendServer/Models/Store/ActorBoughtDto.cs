using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Store
{
    public class ActorBoughtDto
    {
        public string Error { get; set; }
        public ActorDto Actor { get; set; }
    }
}
