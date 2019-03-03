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

        public string Id { get { return id; } set { id = value; } }
        public string[] Tags { get { return tags; } set { tags = value; } }

        public TaggingNative (string id, string[] tags)
        {
            this.id = id;
            this.tags = tags;
        }
    }
}
