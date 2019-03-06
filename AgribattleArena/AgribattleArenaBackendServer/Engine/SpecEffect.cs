using AgribattleArenaBackendServer.Engine.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine
{
    public class SpecEffect : GameObject
    {
        float duration;
        float mod;
        EffectNative native;

        public float Duration { get { return duration; } set { duration = value; } }
        public float Mod { get { return mod; } set { mod = value; } }
        public EffectNative Native { get { return native; } set { native = value; } }

        public SpecEffect(Scene parent, float x, float y, float? z, EffectNative native, float? duration, float? mod)
            : base(parent, x, y, z ?? native.DefaultZ)
        {
            this.duration = duration?? native.DefaultDuration;
            this.native = native;
            this.mod = mod ?? native.DefaultMod;
        }


        public override void Update(float time)
        {
            //this.native.Action?.Invoke(Parent, this, mod, time);
            if (Native.Action != null)
            {
                Jint.Engine actionEngine = new Jint.Engine();
                actionEngine
                    .SetValue("scene", Parent)
                    .SetValue("act", this)
                    .SetValue("mod", mod)
                    .SetValue("time", time)
                    .Execute(Native.Action);
            }
            if (this.duration <= 0) IsAlive = false;
            else this.duration-=time;
        }
    }
}
