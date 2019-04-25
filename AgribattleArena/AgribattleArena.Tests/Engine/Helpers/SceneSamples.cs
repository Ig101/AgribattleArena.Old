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
            nativeManager.AddTileNative("test_wall", new string[] { }, false, 100, true, 1, new string[] { }, new string[] { });
            nativeManager.AddTileNative("test_tile_h1", new string[] { }, false, 9, false, 1, new string[] { }, new string[] { });
            nativeManager.AddTileNative("test_tile_h2", new string[] { }, false, 18, false, 1, new string[] { }, new string[] { });
            nativeManager.AddTileNative("test_tile_h3", new string[] { }, false, 27, false, 1, new string[] { }, new string[] { });
            nativeManager.AddTileNative("test_tile_h4", new string[] { }, false, 36, false, 1, new string[] { }, new string[] { });
            nativeManager.AddTileNative("test_tile_h5", new string[] { }, false, -9, false, 1, new string[] { }, new string[] { });
            nativeManager.AddTileNative("test_tile_h6", new string[] { }, false, -18, false, 1, new string[] { }, new string[] { });
            nativeManager.AddTileNative("test_tile_h7", new string[] { }, false, -27, false, 1, new string[] { }, new string[] { });
            nativeManager.AddTileNative("test_tile_h8", new string[] { }, false, -36, false, 1, new string[] { }, new string[] { });
            nativeManager.AddTileNative("test_tile", new string[] { }, false, 0, false, 1, new string[] { }, new string[] { });
            nativeManager.AddActorNative("test_actor", new string[] { "test_actor_tag" }, 0, new TagSynergy[] { new TagSynergy("test_skill_tag",0.5f)});
            nativeManager.AddSkillNative("test_actor_attack", new string[] { }, 1, 1, 0, 75, new string[] { "DoDamageAttack" });
            nativeManager.AddSkillNative("test_actor_attack_range", new string[] { }, 4, 1, 0, 12.5f, new string[] { "DoDamageAttack" });
            nativeManager.AddSkillNative("test_actor_skill", new string[] { "test_skill_tag" }, 1, 2, 0, 60, new string[] { "DoDamageSkill" });
            nativeManager.AddSkillNative("test_actor_skill_range", new string[] { }, 4, 2, 2, 25, new string[] { "DoDamageSkill" });
            string[,] tileSet = new string[20, 20];
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    if (x == 17 && y == 3) tileSet[x, y] = "test_tile_h1";
                    else if (x == 17 && y == 4) tileSet[x, y] = "test_tile_h2";
                    else if (x == 16 && y == 4) tileSet[x, y] = "test_tile_h3";
                    else if (x == 16 && y == 3) tileSet[x, y] = "test_tile_h4";
                    else if (x == 15 && y == 3) tileSet[x, y] = "test_tile_h5";
                    else if (x == 15 && y == 4) tileSet[x, y] = "test_tile_h6";
                    else if (x == 14 && y == 4) tileSet[x, y] = "test_tile_h7";
                    else if (x == 14 && y == 3) tileSet[x, y] = "test_tile_h8";
                    else if (x == 0 || y == 0 || x == 19 || y == 19)
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
                EngineHelper.CreateActorForGeneration(1,"test_actor","test_actor_attack_range",20,10,20,10,new string[] {"test_actor_skill", "test_actor_skill_range"},
                5, new string[0])
            };
            IActor[] secondPlayerActors = new IActor[]
            {
                EngineHelper.CreateActorForGeneration(2,"test_actor","test_actor_attack",10,20,10,18,new string[] {"test_actor_skill", "test_actor_skill_range"},
                4, new string[0])
            };
            return SceneHelper.CreateNewScene(nativeManager, tileSet, firstPlayerActors, secondPlayerActors, eventHandler);
        }
    }
}
