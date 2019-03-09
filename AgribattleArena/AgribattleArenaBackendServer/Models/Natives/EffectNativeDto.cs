using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Models.Natives
{
    public class EffectNativeDto : TaggingNativeDto
    {
        public float DefaultZ { get; set; }
        public float DefaultDuration { get; set; }
        public float DefaultMod { get; set; }
        public ActionNativeDto Action { get; set; }
    }
}
