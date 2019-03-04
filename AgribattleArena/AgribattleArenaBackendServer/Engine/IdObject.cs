using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine
{
    public abstract class IdObject
    {
        uint id;

        public uint Id { get { return id; } }

        public IdObject (Scene parent)
        {
            id = parent.GetNextId();
        }
    }
}
