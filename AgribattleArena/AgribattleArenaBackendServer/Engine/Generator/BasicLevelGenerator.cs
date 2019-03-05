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
            List<GenerationObject> actors = new List<GenerationObject>();
            List<GenerationObject> decorations = new List<GenerationObject>();

            return new GenerationSet(tileSet, actors,decorations);
        }
    }
}
