using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Models.Natives
{
    public class SkillNative : TaggingNative
    {
        public int DefaultRange { get; set; }
        public int DefaultCost { get; set; }
        public float DefaultCd { get; set; }
        public float DefaultMod { get; set; }
        public ActionNative Action { get; set; }
    }
}
