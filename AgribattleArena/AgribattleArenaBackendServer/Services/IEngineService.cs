using AgribattleArenaBackendServer.Engine.NativeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public interface IEngineService
    {
        void ReinitializeNatives(INativesServiceSceneLink nativesService);
    }
}
