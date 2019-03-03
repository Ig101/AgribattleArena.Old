using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Natives
{
    public class EffectNative : TaggingNative
    {
        SceneObjectAction action;
        float defaultMod;
        float defaultDuration;

        public float DefaultDuration { get { return defaultDuration; } }
        public float DefaultMod { get { return defaultMod; } }
        public SceneObjectAction Action { get { return action; } }

        public EffectNative(string id, string[] tags, SceneObjectAction action, float defaultDuration, float defaultMod)
            : base(id, tags)
        {
            this.defaultDuration = defaultDuration;
            this.defaultMod = defaultMod;
            this.action = action;
        }
    }
}
