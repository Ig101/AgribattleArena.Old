using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Natives
{
    public class SpecEffectDto
    {
        public string Id { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public float Z { get; set; }
        public float Duration { get; set; }
        public float Mod { get; set; }
        public IEnumerable<string> Effects { get; set; }
        public IEnumerable<string> OnDeathEffects { get; set; }
    }
}
