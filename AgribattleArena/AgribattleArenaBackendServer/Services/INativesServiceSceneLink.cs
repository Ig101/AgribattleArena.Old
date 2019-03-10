using AgribattleArenaBackendServer.Models.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public interface INativesServiceSceneLink
    {
        IDictionary<string, TaggingNativeDto> GetAllNatives();
    }
}
