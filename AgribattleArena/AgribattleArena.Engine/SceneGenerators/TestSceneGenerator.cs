using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgribattleArena.Engine.ForExternalUse.Generation.ObjectInterfaces;
using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Natives;
using AgribattleArena.Engine.Objects;
using AgribattleArena.Engine.Objects.Immaterial;

namespace AgribattleArena.Engine.SceneGenerators
{
    class TestSceneGenerator : ISceneGenerator, ForExternalUse.Generation.ISceneGenerator
    {
        public Scene.DefeatCondition DefeatCondition { get { return DefeatConditionTest; } }
        public Scene.WinCondition WinCondition { get { return WinConditionTest; } }
        public string Definition { get { return "TestScene"; } }

        public TestSceneGenerator()
        {

        }

        public void GenerateNewScene(ISceneForSceneGenerator scene, IEnumerable<IPlayer> players, int seed)
        {
            Tile[][] sceneTiles = scene.SetupEmptyTileSet(20, 20);
            for(int x = 0; x<sceneTiles.Length;x++)
            {
                for(int y = 0; y<sceneTiles[x].Length;y++)
                {
                    if(x == 0 || y == 0 || x==sceneTiles.Length-1 || y == sceneTiles[0].Length-1)
                    {
                        scene.CreateTile("tile_test_wall",x,y,null);
                    }
                    else
                    {
                        scene.CreateTile("tile_test", x, y, null);
                    }
                }
            }
            List<IPlayer> tempPlayers = players.ToList();
            if (tempPlayers.Count != 2) throw new ArgumentException("Players count should be 2", "players");
            for(int i = 0; i<2;i++)
            {
                if (tempPlayers[i].KeyActorsGen.Count > 5)
                    throw new ArgumentException("Actors count should be less than 5. Thrown on player " + tempPlayers[i].Id, "players.keyActors");
                Player tempScenePlayer = GeneratorHelper.ConvertExternalPlayerFromGeneration(scene, tempPlayers[i]);
                for(int j = 0; j<tempPlayers[i].KeyActorsGen.Count;j++)
                {
                    tempScenePlayer.KeyActors.Add(GeneratorHelper.ConvertExternalActorFromGeneration(scene, tempScenePlayer,
                        sceneTiles[2 + i * 15][2 + j * 2], tempPlayers[i].KeyActorsGen[j], null));
                }
            }
        }

        public static bool DefeatConditionTest (ISceneParentRef scene, IPlayerParentRef player)
        {
            return false;
        }

        public static bool WinConditionTest (ISceneParentRef scene)
        {
            return false;
        }
    }
}
