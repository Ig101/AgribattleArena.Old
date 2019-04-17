using System;
using System.Collections.Generic;
using System.Text;
using AgribattleArena.Engine.ForExternalUse.Generation.ObjectInterfaces;

namespace AgribattleArena.Engine.SceneGenerators
{
    class DuelSceneGenerator : ISceneGenerator, ForExternalUse.Generation.ISceneGenerator
    {
        public Scene.DefeatCondition DefeatCondition => throw new NotImplementedException();

        public Scene.WinCondition WinCondition => throw new NotImplementedException();

        public void GenerateNewScene(ISceneForSceneGenerator scene, IEnumerable<IPlayer> players, int seed)
        {
            throw new NotImplementedException();
        }
    }
}
