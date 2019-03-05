using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Synchronization
{
    public class SynchronizationObject: ISynchronizationObject
    {
        List<TileObject> changedActors;
        List<Tile> changedTiles;
        List<SpecEffect> deletedEffects;
        List<TileObject> deletedActors;
        Point tileLength;
        int randomCounter;

        public int RandomCounter { get { return randomCounter; } }
        public List<TileObject> ChangedActors { get { return changedActors; } }
        public List<SpecEffect> ChangedEffects { get { return new List<SpecEffect>(); } }
        public List<TileObject> DeletedActors { get { return deletedActors; } }
        public List<SpecEffect> DeletedEffects { get { return deletedEffects; } }
        public List<Tile> ChangedTiles { get { return changedTiles; } }
        public Tile[,] TileSet
        {
            get
            {
                Tile[,] tiles = new Tile[tileLength.X, tileLength.Y];
                foreach(Tile tile in changedTiles)
                {
                    tiles[tile.X, tile.Y] = tile;
                }
                return tiles;
            }
        }

        public SynchronizationObject (List<TileObject> actors, List<TileObject> deletedActors, List<SpecEffect> deletedEffects, Tile[,] tiles, int randomCounter)
        {
            this.randomCounter = randomCounter;
            tileLength = new Point(tiles.GetLength(0), tiles.GetLength(1));
            this.changedActors = new List<TileObject>();
            this.changedActors.AddRange(actors.FindAll(x => x.Affected));
            this.changedTiles = new List<Tile>();
            foreach (Tile tile in tiles)
            {
                if(tile.Affected) this.changedTiles.Add(tile);
            }
            this.deletedActors = new List<TileObject>();
            this.deletedActors.AddRange(deletedActors);
            this.deletedEffects = new List<SpecEffect>();
            this.deletedEffects.AddRange(deletedEffects);
        }
    }
}
