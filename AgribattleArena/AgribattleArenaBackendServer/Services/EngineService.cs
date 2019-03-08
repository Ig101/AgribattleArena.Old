using AgribattleArenaBackendServer.Contexts;
using AgribattleArenaBackendServer.Engine;
using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Engine.NativeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public class EngineService: IEngineService
    {
        INativeManager nativeManager;
        ILevelGenerator levelGenerator;
        List<IScene> scenes;

        public EngineService(INativesServiceSceneLink nativesService)
        {
            nativeManager = new NativeManager(nativesService);
            levelGenerator = new BasicLevelGenerator(40, 40);
            scenes = new List<IScene>();
        }

        public Scene AddNewScene(int id, List<int> players, IProfilesServiceSceneLink profilesService, int seed)
        {
            Scene scene = new Scene(id, players, profilesService, levelGenerator, nativeManager, seed);
            scenes.Add(scene);
            return scene;
        }

        public void ReinitializeNatives(INativesServiceSceneLink nativesService)
        {
            nativeManager.Initialize(nativesService);
        }

        public void SynchronizeHandler (IScene sender, Engine.Helpers.Action action, uint? id, int? actionId, int? targetX, int? targetY)
        {
            
        }
    }
}
