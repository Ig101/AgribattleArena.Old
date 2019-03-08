using AgribattleArenaBackendServer.Contexts;
using AgribattleArenaBackendServer.Engine.NativeManager;
using AgribattleArenaBackendServer.Engine.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public class NativesService : INativesService, INativesServiceSceneLink
    {
        NativesContext context;

        public NativesService(NativesContext context)
        {
            this.context = context;
        }

        public Dictionary<string, TaggingNative> GetAllNatives()
        {
            throw new NotImplementedException();
        }
    }
}
