﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgribattleArena.Engine.ForExternalUse.Generation.ObjectInterfaces;
using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Objects;

namespace AgribattleArena.Engine.SceneGenerators
{
    //TODO Implement
    public class DuelSceneGenerator : ISceneGenerator, ForExternalUse.Generation.ISceneGenerator
    {
        public Scene.DefeatConditionMethod DefeatCondition { get { return DefeatConditionDuel; } }
        public Scene.WinConditionMethod WinCondition { get { return WinConditionDuel; } }
        public string Definition { get { return "DuelScene"; } }

        public DuelSceneGenerator()
        {

        }

        public void GenerateNewScene(ISceneForSceneGenerator scene, IEnumerable<IPlayer> players, int seed)
        {
            Tile[][] sceneTiles = scene.SetupEmptyTileSet(19, 19);
            for (int x = 0; x < sceneTiles.Length; x++)
            {
                for (int y = 0; y < sceneTiles[x].Length; y++)
                {
                    scene.CreateTile("tile_grass", x, y, null);
                }
            }
            List<IPlayer> tempPlayers = players.ToList();
            if (tempPlayers.Count != 2) throw new ArgumentException("Players count should be 2", "players");
            for (int i = 0; i < 2; i++)
            {
                if (tempPlayers[i].KeyActorsGen.Count != 5)
                    throw new ArgumentException("Actors count should be 5. Thrown on player " + tempPlayers[i].Id, "players.keyActors");
                Player tempScenePlayer = GeneratorHelper.ConvertExternalPlayerFromGeneration(scene, tempPlayers[i], i);
                for (int j = 0; j < tempPlayers[i].KeyActorsGen.Count; j++)
                {
                    tempScenePlayer.KeyActors.Add(GeneratorHelper.ConvertExternalActorFromGeneration(scene, tempScenePlayer,
                        sceneTiles[1 + i * 16][1 + j * 4], tempPlayers[i].KeyActorsGen[j], null));
                }
            }
        }

        public static bool DefeatConditionDuel (ISceneParentRef scene, IPlayerParentRef player)
        {
            foreach (Actor actor in player.KeyActors)
            {
                if (actor.IsAlive) return false;
            }
            return true;
        }

        public static bool WinConditionDuel (ISceneParentRef scene)
        {
            int countOfRemainedPlayers = 0;
            foreach (Player player in scene.Players)
            {
                if (player.Status == PlayerStatus.Playing)
                {
                    countOfRemainedPlayers++;
                }
            }
            return countOfRemainedPlayers <= 1;
        }
    }
}
