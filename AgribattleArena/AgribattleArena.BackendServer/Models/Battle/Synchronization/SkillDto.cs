using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Battle.Synchronization
{
    public class SkillDto
    {
        public int Id { get; set; }
        public int Range { get; set; }
        public string NativeId { get; set; }
        public float Cd { get; set; }
        public float Mod { get; set; }
        public int Cost { get; set; }
        public float PreparationTime { get; set; }
    }
}
