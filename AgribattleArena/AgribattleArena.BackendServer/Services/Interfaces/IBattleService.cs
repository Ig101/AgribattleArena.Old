using AgribattleArena.BackendServer.Models.Battle;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Services.Interfaces
{
    public interface IBattleService
    { 
        bool Enqueue(ProfileToEnqueueDto profile);
        bool Dequeue(string profileId);
        ProfileBattleInfoDto GetProfileBattleStatus(string profileId);

        void QueueProcessing();
        void EngineTimeProcessing(int time);
    }
}
