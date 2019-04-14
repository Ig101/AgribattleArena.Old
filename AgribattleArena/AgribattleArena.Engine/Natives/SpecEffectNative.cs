using AgribattleArena.Engine.Helpers.DelegateLists;
using System;
using System.Reflection;

namespace AgribattleArena.Engine.Natives
{
    class SpecEffectNative: TaggingNative
    {
        public float DefaultZ { get; }
        public float DefaultDuration { get; }
        public float DefaultMod { get; }
        public SpecEffectActions.Action Action { get; }

        public SpecEffectNative (string id, string[] tags, float defaultZ, float defaultDuration, float defaultMod, string actionName)
            :this(id, tags, defaultZ, defaultDuration, defaultMod, (SpecEffectActions.Action)Delegate.CreateDelegate(typeof(SpecEffectActions.Action), typeof(SpecEffectActions).GetMethod(actionName, BindingFlags.Public | BindingFlags.Static)))
        {

        }

        public SpecEffectNative(string id, string[] tags, float defaultZ, float defaultDuration, float defaultMod, SpecEffectActions.Action action)
            :base(id, tags)
        {
            this.DefaultZ = defaultZ;
            this.DefaultDuration = defaultDuration;
            this.DefaultMod = defaultMod;
            this.Action = action;
        }
    }
}
