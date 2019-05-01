using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Natives
{
    public class ActorDto
    {
        public string Id { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public float Z { get; set; }
        public IEnumerable<TagSynergyDto> Armor { get; set; }
    }
}
