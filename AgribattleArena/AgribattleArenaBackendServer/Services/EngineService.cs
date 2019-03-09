using AgribattleArenaBackendServer.Contexts;
using AgribattleArenaBackendServer.Engine;
using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities;
using AgribattleArenaBackendServer.Engine.NativeManager;
using AgribattleArenaBackendServer.Engine.Synchronization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public class EngineService: IEngineService
    {
        Random globalRandom;
        INativeManager nativeManager;
        ILevelGenerator levelGenerator;
        List<IScene> scenes;

        public EngineService(INativesServiceSceneLink nativesService)
        {
            globalRandom = new Random();
            nativeManager = new NativeManager(nativesService);
            levelGenerator = new BasicLevelGenerator(40, 40);
            scenes = new List<IScene>();
        }

        public bool AddNewScene(int id, List<int> players, List<PlayerActor> playerActors, int seed)
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

        public void SynchronizeHandler(IScene sender, Engine.Helpers.Action action, 
            uint? id, int? actionId, int? targetX, int? targetY, ISynchronizationObject synchronizationObject)
        {
            //TODO WebsocketsAction
        }

        public int GetNextRandomNumber()
        {
            return globalRandom.Next();
        }
    }
}
