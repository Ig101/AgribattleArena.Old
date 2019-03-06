using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Natives
{
    public class ActorNative : TaggingNative
    {
        float defaultZ;

        public float DefaultZ { get { return defaultZ; } }

        public ActorNative(string id, string[] tags, float defaultZ) : base(id, tags)
        {
            this.defaultZ = defaultZ;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
