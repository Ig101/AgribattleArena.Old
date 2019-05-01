using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Natives
{
    public class TagSynergyDto
    {
        public string SelfTag { get; set; }
        public string TargetTag { get; set; }
        public float Mod { get; set; }
    }
}
