using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Natives
{
    public class DecorationDto
    {
        public string Id { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public int Health { get; set; }
        public float Z { get; set; }
        public float Mod { get; set; }
        public IEnumerable<TagSynergyDto> Armor { get; set; }
        public IEnumerable<string> Action { get; set; }
        public IEnumerable<string> OnDeathAction { get; set; }
    }
}
