using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Synchronization
{
    public class FullSynchronizationObject: ISynchronizationObject
    {
        List<TileObject> actors;
        List<SpecEffect> effects;
        Tile[,] tiles;

        public List<TileObject> ChangedActors { get { return actors; } }
        public List<SpecEffect> ChangedEffects { get { return effects; } }
        public List<TileObject> DeletedActors { get { return new List<TileObject>(); } }
        public List<SpecEffect> DeletedEffects { get { return new List<SpecEffect>(); } }
        public List<Tile> ChangedTiles => tiles.Cast<Tile>().ToList();
        public Tile[,] TileSet { get { return tiles; } }

        public FullSynchronizationObject (List<TileObject> actors, List<SpecEffect> effects, Tile[,] tiles)
        {
            this.actors = actors;
            this.effects = effects;
            this.tiles = tiles;
        }
    }
}
