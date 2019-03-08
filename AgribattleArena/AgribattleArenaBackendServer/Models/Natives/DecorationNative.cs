using AgribattleArenaBackendServer.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Models.Natives
{
    public class DecorationNative : TaggingNative
    {
        public List<TagSynergy> DefaultArmor { get; set; }
        public int DefaultHealth { get; set; }
        public float DefaultZ { get; set; }
        public float DefaultMod { get; set; }
        public ActionNative Action { get; set; }
    }
}
