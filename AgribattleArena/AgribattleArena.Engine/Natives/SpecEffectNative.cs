using AgribattleArena.Engine.Helpers.DelegateLists;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace AgribattleArena.Engine.Natives
{
    public class SpecEffectNative: TaggingNative
    {
        public float DefaultZ { get; }
        public float DefaultDuration { get; }
        public float DefaultMod { get; }
        public SpecEffectActions.Action Action { get; }

        public SpecEffectNative (string id, string[] tags, float defaultZ, float defaultDuration, float defaultMod, IEnumerable<string> actionNames)
            :this(id, tags, defaultZ, defaultDuration, defaultMod, actionNames.Select(actionName => 
            (SpecEffectActions.Action)Delegate.CreateDelegate(typeof(SpecEffectActions.Action), typeof(SpecEffectActions).GetMethod(actionName, BindingFlags.Public | BindingFlags.Static))))
        {

        }

        public SpecEffectNative(string id, string[] tags, float defaultZ, float defaultDuration, float defaultMod, IEnumerable<SpecEffectActions.Action> actions)
            :base(id, tags)
        {
            this.DefaultZ = defaultZ;
            this.DefaultDuration = defaultDuration;
            this.DefaultMod = defaultMod;
            this.Action = null;
            foreach (SpecEffectActions.Action action in actions)
            {
                this.Action += action;
            }
        }
    }
}
