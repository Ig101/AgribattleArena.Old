using AgribattleArenaBackendServer.Models.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public interface IQueueingStorageService
    {
        bool Enqueue(string mode, BattleUserDto user);
        bool Dequeue(string userId);
    }
}
