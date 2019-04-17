using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.SceneGenerators
{
    public interface ISceneGenerator
    {
        Scene.DefeatCondition DefeatCondition { get; }
        Scene.WinCondition WinCondition { get; }
        void GenerateNewScene(ISceneForSceneGenerator scene, IEnumerable<ForExternalUse.Generation.ObjectInterfaces.IPlayer> players, int seed);
    }
}
