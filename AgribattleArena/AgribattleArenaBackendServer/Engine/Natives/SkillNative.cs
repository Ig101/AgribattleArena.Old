using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Natives
{
    public class SkillNative : TaggingNative
    {
        int defaultCost;
        float defaultCd;
        float defaultMod;
        int defaultRange;
        string action;

        public int DefaultRange { get { return defaultRange; } }
        public int DefaultCost { get { return defaultCost; } }
        public float DefaultCd { get { return defaultCd; } }
        public float DefaultMod { get { return defaultMod; } }
        public string Action { get { return action; } }

        public SkillNative(string id, string[] tags, string action, int cost, float cd, float mod, int range) 
            : base(id, tags)
        {
            this.defaultRange = range;
            this.defaultMod = mod;
            this.defaultCd = cd;
            this.defaultCost = cost;
            this.action = action;
        }
    }
}
