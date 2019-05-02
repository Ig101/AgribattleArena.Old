using AgribattleArena.BackendServer.Models.Battle;
using AgribattleArena.BackendServer.Services.Interfaces;
using AgribattleArena.Engine.ForExternalUse.EngineHelper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AgribattleArena.BackendServer.Helpers;
using AgribattleArena.BackendServer.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace AgribattleArena.BackendServer.Services
{
    public class BattleService : IBattleService
    {
        IServiceScopeFactory _serviceScopeFactory;
        IHubContext<BattleHub> _battleHub;

        Dictionary<string, SceneModeQueueDto> _queues;

        public BattleService (IServiceScopeFactory serviceScopeFactory, IHubContext<BattleHub> battleHub)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _battleHub = battleHub;
            _queues = new Dictionary<string, SceneModeQueueDto>()
            {
                { "main_duel", new SceneModeQueueDto()
                    {
                        Queue = new List<ProfileQueueDto>(),
                        Mode = new SceneModeDto()
                        {
                            Generator = EngineHelper.CreateDuelSceneGenerator(),
                            BattleResultProcessor = BattleResultDelegates.ProcessMainDuelBattleResult,
                            MaxPlayers = 2
                        }
                    }
                }
            };
        }

        public ProfileBattleInfoDto GetProfileBattleStatus(string profileId)
        {
            var profileEngineInfoDto = IsUserInBattle(profileId);
            if (profileEngineInfoDto != null)
            {
                return new ProfileBattleInfoDto()
                {
                    Status = ProfileBattleStatus.Battle,
                    BattleInfo = profileEngineInfoDto
                };
            }
            var profileQueueInfoDto = IsUserInQueue(profileId);
            if(profileQueueInfoDto!=null)
            {
                return new ProfileBattleInfoDto()
                {
                    Status = ProfileBattleStatus.Queue,
                    QueueInfo = profileQueueInfoDto
                };
            }
            return new ProfileBattleInfoDto()
            {
                Status = ProfileBattleStatus.Lobby
            };
        }

        #region Queue
        public void QueueProcessing()
        {

        }

        public bool Enqueue(ProfileToEnqueueDto profile)
        {
            if (_queues.Keys.Contains(profile.Mode) && 
                GetProfileBattleStatus(profile.ProfileId).Status==ProfileBattleStatus.Lobby)
            {
                SceneModeQueueDto targetQueue = _queues[profile.Mode];
                targetQueue.Queue.Add(new ProfileQueueDto()
                {
                    ProfileId = profile.ProfileId,
                    Revelations = profile.Revelations
                });
                return true;
            }
            return false;
        }

        public bool Dequeue(string profileId)
        {
            foreach (SceneModeQueueDto queue in _queues.Values)
            {
                ProfileQueueDto user;
                if ((user = queue.Queue.Find(x => x.ProfileId == profileId)) != null)
                {
                    queue.Queue.Remove(user);
                    return true;
                }
            }
            return false;
        }

        ProfileQueueInfoDto IsUserInQueue(string profileId)
        {
            return null;
        }
        #endregion

        #region Engine
        public void EngineTimeProcessing(int seconds)
        {

        }

        ProfileEngineInfoDto IsUserInBattle(string profileId)
        {
            return null;
        }
        #endregion
    }
}
