using AgribattleArena.Engine.ForExternalUse.Synchronization;
using AgribattleArena.Engine.ForExternalUse.Synchronization.ObjectInterfaces;
using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Objects.Abstract;
using AgribattleArena.Engine.Synchronizers.SynchronizationObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgribattleArena.Engine.Synchronizers
{
    public class Synchronizer : ISynchronizer
    {
        IEnumerable<IPlayer> players;
        IEnumerable<IActor> changedActors;
        IEnumerable<IActiveDecoration> changedDecorations;
        IEnumerable<ISpecEffect> changedEffects;
        IEnumerable<ITile> changedTiles;
        IEnumerable<ISpecEffect> deletedEffects;
        IEnumerable<IActor> deletedActors;
        IEnumerable<IActiveDecoration> deletedDecorations;
        IActor tempActor;
        IActiveDecoration tempDecoration;
        Point tileLength;
        int randomCounter;

        public IActiveDecoration TempDecoration { get { return tempDecoration; } }
        public IActor TempActor { get { return tempActor; } }
        public int RandomCounter { get; }
        public IEnumerable<IPlayer> Players { get { return players; } }
        public IEnumerable<IActor> ChangedActors { get { return changedActors; } }
        public IEnumerable<IActiveDecoration> ChangedDecorations { get { return changedDecorations; } }
        public IEnumerable<ISpecEffect> ChangedEffects { get { return changedEffects; } }
        public IEnumerable<IActor> DeletedActors { get { return deletedActors; } }
        public IEnumerable<IActiveDecoration> DeletedDecorations { get { return deletedDecorations; } }
        public IEnumerable<ISpecEffect> DeletedEffects { get { return deletedEffects; } }
        public IEnumerable<ITile> ChangedTiles { get { return changedTiles; } }
        public ITile[,] TileSet
        {
            get
            {
                Tile[,] tiles = new Tile[tileLength.X,tileLength.Y];
                foreach (Tile tile in changedTiles)
                {
                    tiles[tile.X, tile.Y] = tile;
                }
                return tiles;
            }
        }


        public Synchronizer(TileObject tempObject, List<Player> players, List<Objects.Actor> changedActors, List<Objects.ActiveDecoration> changedDecorations,
            List<Objects.SpecEffect> changedEffects, List<Objects.Actor> deletedActors, List<Objects.ActiveDecoration> deletedDecorations, 
            List<Objects.SpecEffect> deletedEffects, Point tileLength, List<Objects.Tile> changedTiles, int randomCounter)
        {
            if (tempObject is Objects.Actor) this.tempActor = new Actor((Objects.Actor)tempObject);
            if (tempObject is Objects.ActiveDecoration) this.tempDecoration = new ActiveDecoration((Objects.ActiveDecoration)tempObject);
            this.randomCounter = randomCounter;
            this.tileLength = tileLength;
            this.players = players.Select(x => new SynchronizationObjects.Player(x));
            this.changedActors = changedActors.Select(x => new Actor(x));
            this.changedDecorations = changedDecorations.Select(x => new ActiveDecoration(x));
            this.changedEffects = changedEffects.Select(x => new SpecEffect(x));
            List<ITile> tempChangedTiles = new List<ITile>();
            this.changedTiles = changedTiles.Select(x => new Tile(x));
            this.deletedActors = deletedActors.Select(x => new Actor(x));
            this.deletedDecorations = deletedDecorations.Select(x => new ActiveDecoration(x));
            this.deletedEffects = deletedEffects.Select(x => new SpecEffect(x));
        }
    }
}
