using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Natives
{
    public class SkillDto
    {
        public string Id { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public int Range { get; set; }
        public int Cost { get; set; }
        public float Cd { get; set; }
        public float Mod { get; set; }
        public IEnumerable<string> Action { get; set; }
    }
}
