using AgribattleArenaBackendServer.Models.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.NativeManager
{
    public interface INativesServiceSceneLink
    {
        IDictionary<string, TaggingNative> GetAllNatives();
    }
}
