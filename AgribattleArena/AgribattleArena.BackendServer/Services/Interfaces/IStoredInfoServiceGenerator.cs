using AgribattleArena.DBProvider.Contexts.ProfileEntities;
using AgribattleArena.BackendServer.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Services.Interfaces
{
    interface IStoredInfoServiceGenerator
    {
        void SetupNew(IServiceProvider services);
    }
}
