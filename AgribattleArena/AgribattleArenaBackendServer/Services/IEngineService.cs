using AgribattleArenaBackendServer.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public interface IEngineService
    {
        void SynchronizeHandler(IScene sender, Engine.Helpers.Action action, uint? id, int? actionId, int? targetX, int? targetY);
    }
}
