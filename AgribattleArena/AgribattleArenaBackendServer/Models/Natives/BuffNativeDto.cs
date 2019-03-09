using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Models.Natives
{
    public class BuffNativeDto : TaggingNativeDto
    {
        public bool Repeatable { get; set; }
        public bool SummarizeLength { get; set; }
        public ActionNativeDto Action { get; set; }
        public ActionNativeDto BuffAplier { get; set; }
        public float? Duration { get; set; }
        public float Mod { get; set; }
    }
}
