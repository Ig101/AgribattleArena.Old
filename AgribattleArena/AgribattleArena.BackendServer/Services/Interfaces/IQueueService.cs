using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Services.Interfaces
{
    public interface IQueueService
    {
        bool Enqueue(string mode, string profileId);
        bool Dequeue(string profileId);
    }
}
