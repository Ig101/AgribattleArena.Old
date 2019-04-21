using System;
using System.Collections.Generic;
using System.Text;
using AgribattleArena.Engine.ForExternalUse;
using AgribattleArena.Engine.ForExternalUse.EngineHelper;
using AgribattleArena.Engine.ForExternalUse.Generation;
using AgribattleArena.Engine.ForExternalUse.Generation.ObjectInterfaces;
using AgribattleArena.Engine.ForExternalUse.Synchronization;

namespace AgribattleArena.Tests.Engine.Helpers
{
    public static class SceneHelper
    {
        #region ActorHelper

        #endregion

        #region TileSetHelper

        #endregion

        public static INativeManager CreateNativeManager()
        {
            INativeManager nativeManager = EngineHelper.CreateNativeManager();

            return nativeManager;
        }

        public static IVarManager CreateVarManager()
        {
            IVarManager varManager = EngineHelper.CreateVarManager(80, 20, 3, 32, 8, 5, 0.1f, 0.1f, 0.1f);

            return varManager;
        }

        public static ISceneGenerator CreateGenerator(string[,] tileSet)
        {
            ISceneGenerator generator = EngineHelper.CreateTestSceneGenerator(tileSet);

            return generator;
        }

        public static IEnumerable<IPlayer> CreatePlayers(IActor[] firstActors, IActor[] secondActors)
        {
            List<IPlayer> players = new List<IPlayer>();
            players.Add(EngineHelper.CreatePlayerForGeneration(1, firstActors));
            players.Add(EngineHelper.CreatePlayerForGeneration(2, secondActors));
            return players;
        }

        public static IScene CreateNewScene (string[,] tileSet, EventHandler<ISyncEventArgs> eventHandler, IActor[] firstPlayer, IActor[] secondPlayer)
        {
            IScene scene = EngineHelper.CreateNewScene(0, CreatePlayers(firstPlayer, secondPlayer), CreateGenerator(tileSet), CreateNativeManager(),
                CreateVarManager(), 0, eventHandler);

            return scene;
        }
    }
}
