using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Helpers.DelegateLists;
using System;
using System.Reflection;

namespace AgribattleArena.Engine.Natives
{
    class ActiveDecorationNative: TaggingNative
    {
        public TagSynergy[] DefaultArmor { get; }
        public int DefaultHealth { get; }
        public float DefaultZ { get; }
        public float DefaultMod { get; }
        public ActiveDecorationActions.Action Action { get; set; }

        public ActiveDecorationNative(string id, string[] tags, TagSynergy[] defaultArmor, int defaultHealth, float defaultZ, float defaultMod, string actionName)
            :this(id, tags, defaultArmor, defaultHealth, defaultZ, defaultMod, (ActiveDecorationActions.Action)Delegate.CreateDelegate(typeof(ActiveDecorationActions.Action), typeof(ActiveDecorationActions).GetMethod(actionName, BindingFlags.Public | BindingFlags.Static)))
        {

        }

        public ActiveDecorationNative(string id, string[] tags, TagSynergy[] defaultArmor, int defaultHealth, float defaultZ, float defaultMod, ActiveDecorationActions.Action action)
            : base(id, tags)
        {
            this.DefaultArmor = defaultArmor;
            this.DefaultHealth = defaultHealth;
            this.DefaultZ = defaultZ;
            this.DefaultMod = defaultMod;
            this.Action = action;
        }
    }
}
