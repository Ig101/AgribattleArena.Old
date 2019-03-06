using AgribattleArenaBackendServer.Engine.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.NativeManager
{
    public interface INativesRepositorySceneLink
    {
        Dictionary<string, TaggingNative> GetAllNatives();
    }
}
