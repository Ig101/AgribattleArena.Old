using AgribattleArenaBackendServer.Engine.NativeManager;
using AgribattleArenaBackendServer.Engine.Natives;
using AgribattleArenaBackendServer.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public class NativesRepository : INativesRepository, INativesRepositorySceneLink
    {
        public Dictionary<string, TaggingNative> GetAllNatives()
        {
            throw new NotImplementedException();
        }
    }
}
