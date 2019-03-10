using AgribattleArenaBackendServer.Contexts;
using AgribattleArenaBackendServer.Engine;
using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities;
using AgribattleArenaBackendServer.Engine.NativeManager;
using AgribattleArenaBackendServer.Engine.Synchronization;
using AgribattleArenaBackendServer.Hubs;
using AgribattleArenaBackendServer.Models.Battle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public class EngineService: IEngineService, IEngineServiceQueueLink, IEngineServiceHubLink
    {
        Random globalRandom;
        INativeManager nativeManager;
        List<IScene> scenes;
        IHubContext<BattleHub> battleHub;

        public EngineService(IHubContext<BattleHub> battleHub, IServiceScopeFactory serviceScopeFactory)
        {
            globalRandom = new Random();
            this.battleHub = battleHub;
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<INativesServiceSceneLink>();
                nativeManager = new NativeManager(scopedProcessingService);
            }
            scenes = new List<IScene>();
        }

        public bool AddNewScene(int id,List<BattleUserDto> players, List<GenerationPartyActor> playerActors, ILevelGenerator levelGenerator, int seed)
        {
            try
            {
                Scene scene = new Scene(id, players, playerActors, levelGenerator, nativeManager, seed);
                scene.ReturnAction += SynchronizeHandler;
                scenes.Add(scene);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ReinitializeNatives(INativesServiceSceneLink nativesService)
        {
            nativeManager.Initialize(nativesService);
        }

        public void CommitAction (int sceneId, Engine.Helpers.ActorAction action)
        {
            IScene tempScene = scenes.Find(x => x.Id == sceneId);
        }

        void SendSynchronizationInfo(IScene scene, SynchronizationInfo info)
        {
            battleHub.Clients.Users(scene.PlayerIds.Select(x => x.UserId).ToList()).SendAsync("SynchronizeBattlefiled", info);
        }

        public void SynchronizeHandler(IScene sender, Engine.Helpers.Action action, 
            uint? id, int? actionId, int? targetX, int? targetY, ISynchronizationObject synchronizationObject)
        {
            SynchronizationInfo info = new SynchronizationInfo();
            SendSynchronizationInfo(sender, info);
        }

        public int GetNextRandomNumber()
        {
            return globalRandom.Next();
        }

        public bool LeaveScene(string id)
        {
            //TODO Leaver
            return true;
        }
    }
}
