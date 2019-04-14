using AgribattleArena.Engine.Helpers.DelegateLists;
using System;
using System.Reflection;

namespace AgribattleArena.Engine.Natives
{
    class BuffNative: TaggingNative
    {
        public bool Repeatable { get; }
        public bool SummarizeLength { get; }
        public BuffActions.Action Action { get; }
        public BuffActions.Applier Applier { get; }
        public float? DefaultDuration { get; }
        public float DefaultMod { get; }

        public BuffNative(string id, string[] tags, bool repeatable, bool summarizeLength, float? defaultDuration, float defaultMod, string actionName, string applierName)
            :this(id, tags, repeatable, summarizeLength, defaultDuration, defaultMod,
                 (BuffActions.Action)Delegate.CreateDelegate(typeof(BuffActions.Action), typeof(BuffActions).GetMethod(actionName, BindingFlags.Public | BindingFlags.Static)),
                 (BuffActions.Applier)Delegate.CreateDelegate(typeof(BuffActions.Applier), typeof(BuffActions).GetMethod(applierName, BindingFlags.Public | BindingFlags.Static)))
        {

        }

        public BuffNative(string id, string[] tags, bool repeatable, bool summarizeLength, float? defaultDuration, float defaultMod, BuffActions.Action action, BuffActions.Applier applier)
            :base(id, tags)
        {
            this.Repeatable = repeatable;
            this.SummarizeLength = summarizeLength;
            this.DefaultDuration = defaultDuration;
            this.DefaultMod = defaultMod;
            this.Action = action;
            this.Applier = applier;
        }
    }
}
