using AgribattleArena.Engine;
using AgribattleArena.Engine.ForExternalUse;
using AgribattleArena.Engine.ForExternalUse.EngineHelper;
using AgribattleArena.Engine.ForExternalUse.Generation.ObjectInterfaces;
using AgribattleArena.Engine.ForExternalUse.Synchronization;
using AgribattleArena.Engine.Helpers;
using AgribattleArena.Tests.Engine.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AgribattleArena.Tests.Engine.Helpers
{
    public static class SceneSamples
    {
        /// <summary>
        /// Scene
        /// tileSet 20x20, test_tile rounded by test_wall
        /// firstPlayer have actor with high strength and constitution
        /// secondPlayer have actor with high willpower and speed
        /// </summary>
        /// <param name="eventHandler"></param>
        /// <returns></returns>
        public static Scene CreateSimpleScene(EventHandler<ISyncEventArgs> eventHandler)
        {
            INativeManager nativeManager = EngineHelper.CreateNativeManager();
            nativeManager.AddTileNative("test_wall", new string[] { }, false, 100, true, 1, new string[] { });
            nativeManager.AddTileNative("test_tile", new string[] { }, false, 0, false, 1, new string[] { });
            nativeManager.AddActorNative("test_actor", new string[] { "living" }, 0, new TagSynergy[] { });
            nativeManager.AddSkillNative("test_actor_attack", new string[] { }, 1, 1, 0, 1, new string[] { });
            string[,] tileSet = new string[20, 20];
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    if (x == 0 || y == 0 || x == 19 || y == 19)
                    {
                        tileSet[x, y] = "test_wall";
                    }
                    else
                    {
                        tileSet[x, y] = "test_tile";
                    }
                }
            }
            IActor[] firstPlayerActors = new IActor[]
            {
                EngineHelper.CreateActorForGeneration(1,"test_actor","test_actor_attack",20,10,20,10,new string[0],4)
            };
            IActor[] secondPlayerActors = new IActor[]
            {
                EngineHelper.CreateActorForGeneration(2,"test_actor","test_actor_attack",10,20,10,20,new string[0],4)
            };
            return SceneHelper.CreateNewScene(nativeManager, tileSet, firstPlayerActors, secondPlayerActors, eventHandler);
        }
    }
}
