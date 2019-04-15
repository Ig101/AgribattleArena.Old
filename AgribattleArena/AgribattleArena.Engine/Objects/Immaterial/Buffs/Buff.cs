using AgribattleArena.Engine.Natives;
using AgribattleArena.Engine.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Objects.Immaterial.Buffs
{
    public class Buff: IdObject
    {
        IBuffManagerParentRef manager;

        BuffNative native;

        float mod;
        float? duration;

        public IBuffManagerParentRef Manager { get { return manager; } }
        public BuffNative Native { get { return native; } }
        public float Mod { get { return mod; } set { mod = value; } }
        public float? Duration { get { return duration; } set { duration = value; } }

        public Buff(IBuffManagerParentRef manager, BuffNative native, float? mod, float? duration)
            : base(manager.Parent.Parent)
        {
            this.mod = mod ?? native.DefaultMod;
            this.duration = duration ?? native.DefaultDuration;
            this.native = native;
            this.manager = manager;
        }

        public void Update(float time)
        {
            native.Action?.Invoke(manager.Parent.Parent, manager.Parent, this, time);
            if (duration != null)
            {
                duration -= time;
            }
        }
    }
}
