using AgribattleArena.Engine.Helpers.DelegateLists;
using System;
using System.Reflection;

namespace AgribattleArena.Engine.Natives
{
    class TileNative: TaggingNative
    {
        public bool Flat { get; }
        public int DefaultHeight { get; }
        public bool Unbearable { get; }
        public TileActions.Action Action { get; }
        public float DefaultMod { get; }

        public TileNative(string id, string[] tags, bool flat, int defaultHeight, bool unbearable, float defaultMod, string actionName)
            :this(id, tags, flat, defaultHeight, unbearable, defaultMod, (TileActions.Action)Delegate.CreateDelegate(typeof(TileActions.Action), typeof(TileActions).GetMethod(actionName, BindingFlags.Public | BindingFlags.Static)))
        {

        }

        public TileNative(string id, string[] tags, bool flat, int defaultHeight, bool unbearable, float defaultMod, TileActions.Action action)
            :base(id,tags)
        {
            this.Flat = flat;
            this.DefaultHeight = defaultHeight;
            this.Unbearable = unbearable;
            this.Action = action;
            this.DefaultMod = defaultMod;
        }
    }
}
