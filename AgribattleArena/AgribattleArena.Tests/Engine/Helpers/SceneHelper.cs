using System;
using System.Collections.Generic;
using System.Text;
using AgribattleArena.Engine;
using AgribattleArena.Engine.ForExternalUse;
using AgribattleArena.Engine.ForExternalUse.EngineHelper;
using AgribattleArena.Engine.ForExternalUse.Generation;
using AgribattleArena.Engine.ForExternalUse.Generation.ObjectInterfaces;
using AgribattleArena.Engine.ForExternalUse.Synchronization;
using AgribattleArena.Engine.NativeManagers;
using AgribattleArena.Engine.SceneGenerators;
using AgribattleArena.Engine.VarManagers;

namespace AgribattleArena.Tests.Engine.Helpers
{
    public static class SceneHelper
    {
        public static NativeManager CreateNativeManager()
        {
            NativeManager nativeManager = (NativeManager)EngineHelper.CreateNativeManager();

            return nativeManager;
        }

        public static VarManager CreateVarManager()
        {
            VarManager varManager = (VarManager)EngineHelper.CreateVarManager(80, 20, 3, 32, 8, 5, 0.1f, 0.1f, 0.1f);

            return varManager;
        }

        public static TestSceneGenerator CreateTestGenerator(string[,] tileSet)
        {
            TestSceneGenerator generator = (TestSceneGenerator)EngineHelper.CreateTestSceneGenerator(tileSet);

            return generator;
        }

        public static IEnumerable<IPlayer> CreatePlayers(IActor[] firstActors, IActor[] secondActors)
        {
            List<IPlayer> players = new List<IPlayer>();
            players.Add(EngineHelper.CreatePlayerForGeneration(1, firstActors));
            players.Add(EngineHelper.CreatePlayerForGeneration(2, secondActors));
            return players;
        }

        public static Scene CreateNewScene (string[,] tileSet, IActor[] firstPlayer, IActor[] secondPlayer)
        {
            Scene scene = (Scene)EngineHelper.CreateNewScene(0, CreatePlayers(firstPlayer, secondPlayer), CreateTestGenerator(tileSet), CreateNativeManager(),
                CreateVarManager(), 0, null);

            return scene;
        }
    }
}
