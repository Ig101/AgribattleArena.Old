using AgribattleArenaBackendServer.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public class EngineService: IEngineService
    {
        List<IScene> scenes;

        public void SynchronizeHandler (IScene sender, Engine.Action action, uint? id, int? actionId, int? targetX, int? targetY)
        {
            
        }
    }
}
