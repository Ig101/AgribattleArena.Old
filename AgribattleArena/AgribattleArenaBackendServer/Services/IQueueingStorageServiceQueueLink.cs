using AgribattleArenaBackendServer.Models.Queueing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public interface IQueueingStorageServiceQueueLink
    {
        List<SceneModeQueueDto> GetFullGroups();
    }
}
