﻿using AgribattleArena.Engine;
using AgribattleArena.Engine.ForExternalUse;
using AgribattleArena.Engine.ForExternalUse.EngineHelper;
using AgribattleArena.Engine.ForExternalUse.Generation.ObjectInterfaces;
using AgribattleArena.Engine.ForExternalUse.Synchronization;
using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Helpers.DelegateLists;
using AgribattleArena.Tests.Engine.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgribattleArena.Tests.Engine.Helpers
{
    public static class SceneSamples
    {
        static void AddNatives(INativeManager nativeManager)
        {
            nativeManager.AddTileNative("test_wall", new string[] { }, false, 100, true, 1, new string[] { }, new string[] { });
            nativeManager.AddTileNative("test_tile_h1", new string[] { }, false, 9, false, 1, new string[] { }, new string[] { });
            nativeManager.AddTileNative("test_tile_h2", new string[] { }, false, 18, false, 1, new string[] { }, new string[] { });
            nativeManager.AddTileNative("test_tile_h3", new string[] { }, false, 27, false, 1, new string[] { }, new string[] { });
            nativeManager.AddTileNative("test_tile_h4", new string[] { }, false, 36, false, 1, new string[] { }, new string[] { });
            nativeManager.AddTileNative("test_tile_h5", new string[] { }, false, -9, false, 1, new string[] { }, new string[] { });
            nativeManager.AddTileNative("test_tile_h6", new string[] { }, false, -18, false, 1, new string[] { }, new string[] { });
            nativeManager.AddTileNative("test_tile_h7", new string[] { }, false, -27, false, 1, new string[] { }, new string[] { });
            nativeManager.AddTileNative("test_tile_h8", new string[] { }, false, -36, false, 1, new string[] { }, new string[] { });
            nativeManager.AddTileNative("test_tile_effect", new string[] { }, false, 0, false, 10, new string[] { "DoDamage" }, new string[] { "DoDamageOnStep" });
            nativeManager.AddTileNative("test_tile", new string[] { }, false, 0, false, 1, new string[] { }, new string[] { });
            nativeManager.AddActorNative("test_actor", new string[] { "test_actor_tag" }, 0, new TagSynergy[] { new TagSynergy("test_skill_tag", 0.5f) });
            nativeManager.AddEffectNative("test_effect", new string[] { }, 0, null, 10, new string[] { "DoDamageTempTile" }, 
                new string[] { "DoDamageTempTileDeath" });
            nativeManager.AddDecorationNative("test_decoration", new string[] { }, new TagSynergy[] { }, 100, 0, 10, new string[] { "DoSelfDamage" },
                new string[] { "DoSelfDamage" });
            nativeManager.AddSkillNative("test_actor_attack", new string[] { }, 1, 1, 0, 75, new string[] { "DoDamageAttack" });
            nativeManager.AddSkillNative("test_actor_attack_range", new string[] { }, 4, 1, 0, 12.5f, new string[] { "DoDamageAttack" });
            nativeManager.AddSkillNative("test_actor_skill", new string[] { "test_skill_tag" }, 1, 1, 0, 60, new string[] { "DoDamageSkill" });
            nativeManager.AddSkillNative("test_actor_skill_range", new string[] { }, 4, 2, 2, 10, new string[] { "DoDamageSkill" });
            nativeManager.AddBuffNative("test_buff_default", new string[] { "buff" }, false, false, false, null, 1, 
                new string[] { }, new string[] { "AddTestAttackAndArmor", "AddStrength", "AddMaxHealth" }, new string[] { "DamageSelfPurge" });
            nativeManager.AddBuffNative("test_buff_duration", new string[] { "buff" }, false, false, false, 1, 1,
                new string[] { }, new string[] { }, new string[] { });
            nativeManager.AddBuffNative("test_buff_eternal", new string[] { "item" }, true, false, false, null, 1,
                new string[] { }, new string[] { "AddMaxHealth" }, new string[] { "DamageSelfPurge" });
            nativeManager.AddBuffNative("test_buff_multiple", new string[] { "buff" }, false, true, false, null, 1,
                new string[] { }, new string[] { "AddMaxHealth" }, new string[] { });
            nativeManager.AddBuffNative("test_buff_summarize", new string[] { "buff" }, false, false, true, null, 1,
                new string[] { }, new string[] { "AddMaxHealth" }, new string[] { });
            nativeManager.AddBuffNative("test_debuff", new string[] { "debuff" }, false, false, false, null, 1,
                new string[] { "DamageSelf"  }, new string[] { }, new string[] { "DamageSelfPurge" });
        }

        /// <summary>
        /// Scene
        /// tileSet 20x20, test_tile rounded by test_wall
        /// firstPlayer have actor with high strength and constitution
        /// secondPlayer have actor with high willpower and speed
        /// </summary>
        /// <param name="eventHandler"></param>
        /// <returns></returns>
        public static Scene CreateSimpleScene(EventHandler<ISyncEventArgs> eventHandler, bool victory)
        {
            INativeManager nativeManager = EngineHelper.CreateNativeManager();
            AddNatives(nativeManager);
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
            return SceneHelper.CreateNewScene(nativeManager, tileSet, victory, firstPlayerActors, secondPlayerActors, eventHandler);
        }
    }
}
