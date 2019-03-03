using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Natives
{
    public class ActorNative : TaggingNative
    {
        public ActorNative(string id, string[] tags) : base(id, tags)
        {
        }
    }
}
