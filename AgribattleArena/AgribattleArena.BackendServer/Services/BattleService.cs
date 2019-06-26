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
using AgribattleArena.Engine.ForExternalUse;
using AgribattleArena.BackendServer.Models.Enum;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace AgribattleArena.BackendServer.Services
{
    public class BattleService : IBattleService
    {
        IServiceScopeFactory _serviceScopeFactory;
        IHubContext<BattleHub> _battleHub;
        ILogger<BattleService> _logger;

        Dictionary<string, SceneModeQueueDto> _queues;
        INativeManager _nativeManager;
        ConstantsConfig _constants;

        public BattleService (IServiceScopeFactory serviceScopeFactory, IHubContext<BattleHub> battleHub,
            IOptions<ConstantsConfig> constants, ILogger<BattleService> logger)
        {
            _constants = constants.Value;
            _serviceScopeFactory = serviceScopeFactory;
            _battleHub = battleHub;
            _queues = BattleHelper.GetNewModeQueue();
            _nativeManager = SetupNativeManager();
            _logger = logger;
        }

        public INativeManager SetupNativeManager()
        {
            INativeManager manager = EngineHelper.CreateNativeManager();
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<INativesRepository>();
                //TODO NativeService
            }
            return manager;
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
        public void QueueProcessing(int time)
        {
            foreach(var queue in _queues.Values)
            {
                List<List<ProfileQueueDto>> complectingActors = new List<List<ProfileQueueDto>>();
                List<List<ProfileQueueDto>> complectedActors = new List<List<ProfileQueueDto>>();
                foreach(var profile in queue.Queue)
                {
                    int difference = profile.Time >= _constants.QueueTimeout ?
                        _constants.QueueRevelationLevelCompareLimitAfterTimeout :
                        _constants.QueueRevelationLevelCompareLimit;
                    bool added = false;
                    for(int j = 0; j<complectingActors.Count;j++)
                    {
                        List<ProfileQueueDto> complect = complectingActors[j];
                        if(Math.Abs(complect.First().RevelationLevel - profile.RevelationLevel)<=difference)
                        {
                            complect.Add(profile);
                            if (complect.Count >= queue.Mode.MaxPlayers)
                            {
                                complectedActors.Add(complect);
                                complectingActors.RemoveAt(j);
                            }
                            added = true;
                            break;
                        }
                    }
                    if(!added)
                    {
                        complectingActors.Add(new List<ProfileQueueDto>() { profile });
                    }
                    profile.Time += time;
                }
                foreach(List<ProfileQueueDto> complect in complectedActors)
                {
                    foreach(ProfileQueueDto profile in complect)
                    {
                        queue.Queue.Remove(profile);
                    }
                    _logger.LogInformation("Start new Scene");
                    StartNewBattle(queue.Mode, complect);
                }
            }
        }

        public bool Enqueue(ProfileToEnqueueEnrichedDto profile)
        {
            if (_queues.Keys.Contains(profile.Mode) && 
                GetProfileBattleStatus(profile.ProfileId).Status==ProfileBattleStatus.Lobby)
            {
                SceneModeQueueDto targetQueue = _queues[profile.Mode];
                targetQueue.Queue.Add(new ProfileQueueDto()
                {
                    ProfileId = profile.ProfileId,
                    RevelationLevel = profile.RevelationLevel
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
            foreach(var queue in _queues)
            {
                foreach(var profile in queue.Value.Queue)
                {
                    if(profileId == profile.ProfileId)
                    {
                        return new ProfileQueueInfoDto()
                        {
                            Mode = queue.Key,
                            Time = profile.Time
                        };
                    }
                }
            }
            return null;
        }
        #endregion

        #region Engine
        public void EngineTimeProcessing(int seconds)
        {

        }

        void StartNewBattle(SceneModeDto mode, List<ProfileQueueDto> profiles)
        {

        }

        ProfileEngineInfoDto IsUserInBattle(string profileId)
        {
            return null;
        }
        #endregion
    }
}
