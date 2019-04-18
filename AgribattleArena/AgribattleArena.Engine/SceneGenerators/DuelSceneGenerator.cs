using System;
using System.Collections.Generic;
using System.Text;
using AgribattleArena.Engine.ForExternalUse.Generation.ObjectInterfaces;

namespace AgribattleArena.Engine.SceneGenerators
{
    //TODO Implement
    class DuelSceneGenerator : ISceneGenerator, ForExternalUse.Generation.ISceneGenerator
    {
        public Scene.DefeatCondition DefeatCondition { get { return DefeatConditionDuel; } }
        public Scene.WinCondition WinCondition { get { return WinConditionDuel; } }

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
