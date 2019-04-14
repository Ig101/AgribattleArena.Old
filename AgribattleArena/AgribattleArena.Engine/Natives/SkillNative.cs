using AgribattleArena.Engine.Helpers.DelegateLists;
using System;
using System.Reflection;

namespace AgribattleArena.Engine.Natives
{
    class SkillNative: TaggingNative
    {
        public int DefaultRange { get; }
        public int DefaultCost { get; }
        public float DefaultCd { get; }
        public float DefaultMod { get; }
        public SkillActions.Action Action { get; }

        public SkillNative(string id, string[] tags, int defaultRange, int defaultCost, float defaultCd, float defaultMod, string actionName)
            :this(id, tags, defaultRange, defaultCost, defaultCd, defaultMod, (SkillActions.Action)Delegate.CreateDelegate(typeof(SkillActions.Action), typeof(SkillActions).GetMethod(actionName, BindingFlags.Public | BindingFlags.Static)))
        {

        }

        public SkillNative(string id, string[] tags, int defaultRange, int defaultCost, float defaultCd, float defaultMod, SkillActions.Action action)
            :base(id, tags)
        {
            this.DefaultRange = defaultRange;
            this.DefaultCost = defaultCost;
            this.DefaultCd = defaultCd;
            this.DefaultMod = defaultMod;
            this.Action = action;
        }
    }
}
