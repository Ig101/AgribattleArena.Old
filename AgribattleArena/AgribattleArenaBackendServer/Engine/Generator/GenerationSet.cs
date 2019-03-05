using AgribattleArenaBackendServer.Engine.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Generator
{
    public class GenerationSet
    {
        GenerationTile[,] tileSet;
        List<GenerationObject> objects;

        public GenerationTile[,] TileSet { get { return tileSet; } }
        public List<GenerationObject> Objects { get { return objects; } }

        public GenerationSet(GenerationTile[,] tileSet, List<GenerationObject> objects)
        {
            this.tileSet = tileSet;
            this.objects = objects;
        }
    }
}
