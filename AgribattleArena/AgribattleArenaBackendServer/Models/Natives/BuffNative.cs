using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Models.Natives
{
    public class BuffNative : TaggingNative
    {
        public bool Repeatable { get; set; }
        public bool SummarizeLength { get; set; }
        public ActionNative Action { get; set; }
        public ActionNative BuffAplier { get; set; }
        public float? Duration { get; set; }
        public float Mod { get; set; }
    }
}
