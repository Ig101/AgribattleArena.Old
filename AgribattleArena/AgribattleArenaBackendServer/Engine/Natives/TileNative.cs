using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Natives
{
    public class TileNative: TaggingNative
    {
        string action;
        float actionMod;
        bool unbearable;
        bool flat;
        int defaultHeight;
        
        public bool Flat { get { return flat; } }
        public int DefaultHeight { get { return defaultHeight; } }
        public bool Unbearable { get { return unbearable; } }
        public string Action { get { return action; } }
        public float ActionMod { get { return actionMod; } }

        public TileNative (string id, string action, float actionMod, bool unbearable, bool flat, int defaultHeight, string[] tags)
            :base(id, tags)
        {
            this.flat = flat;
            this.action = action;
            this.actionMod = actionMod;
            this.unbearable = unbearable;
            this.defaultHeight = defaultHeight;
        }
    }
}
