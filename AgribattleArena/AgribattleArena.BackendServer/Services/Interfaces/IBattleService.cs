using AgribattleArena.BackendServer.Models.Battle;
using AgribattleArena.Engine.ForExternalUse;
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
        bool Enqueue(ProfileToEnqueueEnrichedDto profile);
        bool Dequeue(string profileId);
        ProfileBattleInfoDto GetProfileBattleStatus(string profileId);

        Task QueueProcessing(int time);
        void EngineTimeProcessing(int time);

        IScene GetUserScene(string profileId);
    }
}
