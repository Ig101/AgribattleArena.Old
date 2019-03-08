using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Models.Natives
{
    public class TileNative: TaggingNative
    {
        public bool Flat { get; set; }
        public int DefaultHeight { get; set; }
        public bool Unbearable { get; set; }
        public ActionNative Action { get; set; }
        public float ActionMod { get; set; }
    }
}
