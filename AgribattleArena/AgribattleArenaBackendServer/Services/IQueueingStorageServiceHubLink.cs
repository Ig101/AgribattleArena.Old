using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public interface IQueueingStorageServiceHubLink
    {
        bool Dequeue(string userId);
    }
}
