using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.SceneGenerators
{
    public interface ISceneGenerator
    {
        void GenerateNewScene(ISceneForSceneGenerator scene, IEnumerable<ForExternalUse.Generation.ObjectInterfaces.IPlayer> players, int seed);
    }
}
