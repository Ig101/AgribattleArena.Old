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
    public class TestSceneGenerator : ISceneGenerator, ForExternalUse.Generation.ISceneGenerator
    {
        public Scene.DefeatConditionMethod DefeatCondition { get { return DefeatConditionTest; } }
        public Scene.WinConditionMethod WinCondition { get { return WinConditionTest; } }
        public string Definition { get { return "TestScene"; } }

        string[,] tileSet;

        public TestSceneGenerator(string[,] tileSet)
        {
            if (tileSet.GetLength(0) != 20) throw new ArgumentException("Number of rows doesn't equal to 20");
            if (tileSet.GetLength(1) != 20) throw new ArgumentException("Number of columns doesn't equal to 20");
            this.tileSet = tileSet;
        }

        public void GenerateNewScene(ISceneForSceneGenerator scene, IEnumerable<IPlayer> players, int seed)
        {
            Tile[][] sceneTiles = scene.SetupEmptyTileSet(20, 20);
            for(int x = 0; x<sceneTiles.Length;x++)
            {
                for(int y = 0; y<sceneTiles[x].Length;y++)
                {
                    scene.CreateTile(tileSet[x, y], x, y, null);
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
