using AgribattleArenaBackendServer.Engine.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.ActorModel
{
    public class Buff
    {
        BuffManager manager;

        BuffNative native;

        float mod;
        float? duration;

        public BuffManager Manager { get { return manager; } }
        public BuffNative Native { get { return native; } }
        public float Mod { get { return mod; } set { mod = value; } }
        public float? Duration { get { return duration; } set { duration = value; } }

        public Buff(BuffManager manager, BuffNative native, float? mod, float? duration)
        {
            this.mod = mod ?? native.Mod;
            this.duration = duration ?? native.Duration;
            this.native = native;
            this.manager = manager;
        }
        
        public void Update (float time)
        {
            native.Action?.Invoke(manager.RoleModel.Owner.Parent, manager.RoleModel.Owner, mod, time);
            if(duration!=null)
            {
                duration -= time;
            }
        }
    }
}
