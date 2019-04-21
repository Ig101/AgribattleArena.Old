using System;
using System.Collections.Generic;
using System.Text;
using AgribattleArena.Engine.ForExternalUse.Generation.ObjectInterfaces;

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

        }

        public static bool DefeatConditionDuel (ISceneParentRef scene, IPlayerParentRef player)
        {
            return false;
        }

        public static bool WinConditionDuel (ISceneParentRef scene)
        {
            return false;
        }
    }
}
