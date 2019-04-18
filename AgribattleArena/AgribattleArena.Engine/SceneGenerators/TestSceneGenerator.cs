using System;
using System.Collections.Generic;
using System.Text;
using AgribattleArena.Engine.ForExternalUse.Generation.ObjectInterfaces;

namespace AgribattleArena.Engine.SceneGenerators
{
    class TestSceneGenerator : ISceneGenerator, ForExternalUse.Generation.ISceneGenerator
    {
        public Scene.DefeatCondition DefeatCondition { get { return DefeatConditionTest; } }
        public Scene.WinCondition WinCondition { get { return WinConditionTest; } }

        public TestSceneGenerator()
        {

        }

        public void GenerateNewScene(ISceneForSceneGenerator scene, IEnumerable<IPlayer> players, int seed)
        {

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
