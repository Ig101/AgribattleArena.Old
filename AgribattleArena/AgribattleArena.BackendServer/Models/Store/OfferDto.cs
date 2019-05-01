using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Store
{
    public class OfferDto
    {
        public int Id { get; set; }
        public string ProfileId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<ActorDto> Items { get; set; }
    }
}
