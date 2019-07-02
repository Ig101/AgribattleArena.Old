using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Battle.Synchronization
{
    public class BuffDto
    {
        public int Id { get; set; }
        public string NativeId { get; set; }
        public float Mod { get; set; }
        public float? Duration { get; set; }
    }
}
