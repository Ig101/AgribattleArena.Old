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
using AgribattleArena.DBProvider.Contexts.ProfileEntities;
using AgribattleArena.Engine.ForExternalUse.Generation.ObjectInterfaces;
using AgribattleArena.Engine.ForExternalUse.Synchronization;
using AgribattleArena.BackendServer.Models.Battle.Synchronization;

namespace AgribattleArena.BackendServer.Services
{
    public class BattleService : IBattleService
    {
        IServiceScopeFactory _serviceScopeFactory;
        IHubContext<BattleHub> _battleHub;
        ILogger<BattleService> _logger;
        IProfilesService _profilesService;

        Dictionary<string, SceneModeQueueDto> _queues;
        INativeManager _nativeManager;
        ConstantsConfig _constants;
        List<IScene> _scenes;
        Random _random;
        long _sceneEnumerator;

        public BattleService (IServiceScopeFactory serviceScopeFactory, IHubContext<BattleHub> battleHub,
            IOptions<ConstantsConfig> constants, ILogger<BattleService> logger, Random random, IProfilesService profilesService)
        {
            _random = random;
            _constants = constants.Value;
            _serviceScopeFactory = serviceScopeFactory;
            _battleHub = battleHub;
            _queues = BattleHelper.GetNewModeQueue();
            _nativeManager = SetupNativeManager();
            _logger = logger;
            _scenes = new List<IScene>();
            _sceneEnumerator = 0;
            _profilesService = profilesService;
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
        public async Task QueueProcessing(int time)
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
                    await StartNewBattle(queue.Mode, complect);
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
        void SynchronizationInfoEventHandler(object sender, ISyncEventArgs e)
        {
            string actionName = "Battle" + e.Action == null ? "Info" : e.Action.ToString();
            foreach (IPlayerShort player in e.Scene.ShortPlayers)
            {
                _battleHub.Clients.User(player.Id)?.SendAsync(actionName, BattleHelper.MapSynchronizer(e, player));
            }
            if(e.Action == Engine.Helpers.Action.EndGame)
            {
                _scenes.Remove(e.Scene);
                //TODO Rewards
            }
        }

        public void EngineTimeProcessing(int seconds)
        {
            foreach(var scene in _scenes)
            {
                scene.UpdateTime(seconds);
            }
        }

        async Task StartNewBattle(SceneModeDto mode, List<ProfileQueueDto> profiles)
        {
            List<string> profileIds = profiles.Select(x => x.ProfileId).ToList();
            await _battleHub.Clients.Users(profileIds).SendAsync("BattlePrepare");
            long tempSceneId = _sceneEnumerator;
            _sceneEnumerator++;
            if (_sceneEnumerator == long.MaxValue) _sceneEnumerator = 0;
            List<IPlayer> players = new List<IPlayer>(profiles.Count);
            foreach(string id in profileIds)
            {
                Profile profile = await _profilesService.GetProfileWithInfo(id);
                players.Add(EngineHelper.CreatePlayerForGeneration(id, null, profile.Actors.Select(
                    x => EngineHelper.CreateActorForGeneration(x.Id, x.ActorNative, x.AttackingSkillNative, x.Strength, x.Willpower, x.Constitution, x.Speed,
                    x.Skills.Select(k => k.Native), x.ActionPointsIncome, null))));
            }
            _scenes.Add(EngineHelper.CreateNewScene(tempSceneId, players, mode.Generator, _nativeManager, mode.VarManager, _random.Next(), SynchronizationInfoEventHandler));
        }

        SynchronizerDto IsUserInBattle(string profileId)
        {
            foreach(var scene in _scenes)
            {
                if (scene.ShortPlayers.FirstOrDefault(x => x.Id == profileId) != null)
                {
                    return BattleHelper.GetFullSynchronizationData(scene, profileId);
                }
            }
            return null;
        }
        #endregion
    }
}
