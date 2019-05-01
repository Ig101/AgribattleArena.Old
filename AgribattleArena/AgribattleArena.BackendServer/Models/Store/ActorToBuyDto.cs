using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Store
{
    public class ActorToBuyDto
    {
        public string Name { get; set; }
        public int ActorId { get; set; }
        public int OfferId { get; set; }
    }
}
