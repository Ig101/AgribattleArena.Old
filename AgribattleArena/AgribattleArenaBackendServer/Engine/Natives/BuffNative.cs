using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Natives
{
    public class BuffNative : TaggingNative
    {
        string action;
        string buffAplier;
        float? duration;
        float mod;
        bool summarizeLength;
        bool repeatable;

        public bool Repeatable { get { return repeatable; } }
        public bool SummarizeLength { get { return summarizeLength; } }
        public string Action { get { return action; } }
        public string BuffAplier { get { return buffAplier; } }
        public float? Duration { get { return duration; } }
        public float Mod { get { return mod; } }

        public BuffNative(string id, string[] tags, string action, string buffAplier, float? duration, float mod, bool summarizeLength, bool repeatable) 
            : base(id, tags)
        {
            this.action = action;
            this.buffAplier = buffAplier;
            this.duration = duration;
            this.mod = mod;
            this.summarizeLength = summarizeLength;
            this.repeatable = repeatable;
        }
    }
}
