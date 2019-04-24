﻿using AgribattleArena.Engine.Natives;
using AgribattleArena.Engine.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Objects
{
    public class SpecEffect: GameObject
    {
        float? duration;
        float mod;
        SpecEffectNative native;
        bool affected;

        public bool Affected { get { return affected; } set { affected = value; } }
        public float? Duration { get { return duration; } set { duration = value; } }
        public float Mod { get { return mod; } set { mod = value; } }
        public SpecEffectNative Native { get { return native; } set { native = value; } }

        public SpecEffect(ISceneParentRef parent, IPlayerParentRef owner, int x, int y, float? z, SpecEffectNative native, float? duration, float? mod)
            : base(parent, owner, x, y, z ?? native.DefaultZ)
        {
            this.duration = duration ?? native.DefaultDuration ?? null;
            this.native = native;
            this.mod = mod ?? native.DefaultMod;
            this.affected = true;
        }

        public override void Update(float time)
        {
            this.native.Action?.Invoke(Parent, this, time);
            if (this.duration <= 0) IsAlive = false;
            else this.duration -= time;
        }

        public override void OnDeathAction()
        {
            native.OnDeathAction?.Invoke(Parent, this);
        }
    }
}
