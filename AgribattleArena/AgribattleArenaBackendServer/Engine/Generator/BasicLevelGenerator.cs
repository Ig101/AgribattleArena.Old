using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Generator
{
    public class BasicLevelGenerator : ILevelGenerator
    {
        int x;
        int y;

        public BasicLevelGenerator()
        {
            x = 40;
            y = 40;
        }

        public GenerationSet GenerateNewScene()
        {
            GenerationTile[,] tileSet = new GenerationTile[x,y];
            List<GenerationObject> objects = new List<GenerationObject>();

            return new GenerationSet(tileSet, objects);
        }
    }
}
