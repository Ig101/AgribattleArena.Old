using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Natives
{
    public class DecorationNative : TaggingNative
    {
        float defaultMod;
        SceneObjectAction action;
        
        public float DefaultMod { get { return defaultMod; } }
        public SceneObjectAction Action { get { return action; } }

        public DecorationNative(string id, string[] tags, float defaultMod, SceneObjectAction action) : base(id, tags)
        {
            this.defaultMod = defaultMod;
            this.action = action;
        }
    }
}
