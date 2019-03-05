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
        float defaultZ;
        TagSynergy[] defaultArmor;
        int defaultHealth;

        public TagSynergy[] DefaultArmor { get { return defaultArmor; } }
        public int DefaultHealth { get { return defaultHealth; } }
        public float DefaultZ { get { return defaultZ; } }
        public float DefaultMod { get { return defaultMod; } }
        public SceneObjectAction Action { get { return action; } }

        public DecorationNative(string id, string[] tags, float defaultMod, SceneObjectAction action, float defaultZ, int defaultHealth, TagSynergy[] defaultArmor) 
            : base(id, tags)
        {
            this.defaultHealth = defaultHealth;
            this.defaultArmor = defaultArmor;
            this.defaultZ = defaultZ;
            this.defaultMod = defaultMod;
            this.action = action;
        }
    }
}
