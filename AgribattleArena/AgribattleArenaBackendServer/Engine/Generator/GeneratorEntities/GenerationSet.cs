using AgribattleArenaBackendServer.Engine.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities
{
    public class GenerationSet
    {
        GenerationTile[,] tileSet;
        List<GenerationObject> actors;
        List<GenerationObject> decorations;

        public GenerationTile[,] TileSet { get { return tileSet; } }
        public List<GenerationObject> Actors { get { return actors; } }
        public List<GenerationObject> Decorations { get { return decorations; } }

        public GenerationSet(GenerationTile[,] tileSet, List<GenerationObject> actors, List<GenerationObject> decorations)
        {
            this.tileSet = tileSet;
            this.actors = actors;
            this.decorations = decorations;
        }
    }
}
