using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Natives
{
    public class BuffNative : TaggingNative
    {
        SceneObjectAction action;
        BuffAction buffAplier;
        float? duration;
        float mod;

        public SceneObjectAction Action { get { return action; } }
        public BuffAction BuffAplier { get { return buffAplier; } }
        public float? Duration { get { return duration; } }
        public float Mod { get { return mod; } }

        public BuffNative(string id, string[] tags, SceneObjectAction action, BuffAction buffAplier, float? duration, float mod) 
            : base(id, tags)
        {
            this.action = action;
            this.buffAplier = buffAplier;
            this.duration = duration;
            this.mod = mod;
        }
    }
}
