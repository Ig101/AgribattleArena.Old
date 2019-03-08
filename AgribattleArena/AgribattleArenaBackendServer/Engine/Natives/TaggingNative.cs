using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Natives
{
    public class TaggingNative
    {
        string id;
        string[] tags;

        public string Id { get { return id; } }
        public string[] Tags { get { return tags; } }

        public TaggingNative (string id, string[] tags)
        {
            this.id = id;
            this.tags = tags;
        }
    }
}
