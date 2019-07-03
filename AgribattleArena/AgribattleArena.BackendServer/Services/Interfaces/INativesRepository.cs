using AgribattleArena.BackendServer.Models.Natives.Frontend;
using AgribattleArena.Engine.ForExternalUse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Services.Interfaces
{
    public interface INativesRepository
    {
        INativeManager CreateBackendNativesManager();
        Task<NativesDto> GetFrontendNatives();
    }
}
