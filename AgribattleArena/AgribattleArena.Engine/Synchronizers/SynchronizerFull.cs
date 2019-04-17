using AgribattleArena.Engine.ForExternalUse.Synchronization;
using AgribattleArena.Engine.ForExternalUse.Synchronization.ObjectInterfaces;
using AgribattleArena.Engine.Synchronizers.SynchronizationObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgribattleArena.Engine.Synchronizers
{
    public class SynchronizerFull : ISynchronizer
    {
        IEnumerable<IPlayer> players;

        IEnumerable<IActor> actors;
        IEnumerable<IActiveDecoration> decorations;
        IEnumerable<ISpecEffect> effects;
        ITile[,] tiles;

        int randomCounter;

        public int RandomCounter { get; }
        public IEnumerable<IPlayer> Players { get { return players; } }
        public IEnumerable<IActor> ChangedActors { get { return actors; } }
        public IEnumerable<IActiveDecoration> ChangedDecorations { get { return decorations; } }
        public IEnumerable<ISpecEffect> ChangedEffects { get { return effects; } }
        public IEnumerable<IActor> DeletedActors { get { return new List<IActor>(); } }
        public IEnumerable<IActiveDecoration> DeletedDecorations { get { return new List<IActiveDecoration>(); } }
        public IEnumerable<ISpecEffect> DeletedEffects { get { return new List<ISpecEffect>(); } }
        public IEnumerable<ITile> ChangedTiles { get { return tiles.Cast<ITile>().ToList(); } }
        public ITile[,] TileSet { get { return tiles; } }

        public SynchronizerFull(List<Player> players, List<Objects.Actor> actors, List<Objects.ActiveDecoration> decorations, 
            List<Objects.SpecEffect> effects, Objects.Tile[][] tiles, int randomCounter)
        {
            this.randomCounter = randomCounter;
            this.players = players.Select(x => new SynchronizationObjects.Player(x));
            this.decorations = decorations.Select(x => new ActiveDecoration(x));
            this.actors = actors.Select(x => new Actor(x));
            this.effects = effects.Select(x => new SpecEffect(x));
            this.tiles = new ITile[tiles.Length, tiles[0].Length];
            for (int x = 0; x < tiles.Length; x++)
            {
                for(int y = 0; y<tiles[x].Length;y++)
                {
                    this.tiles[x, y] = new Tile(tiles[x][y]); 
                }
            }
        }
    }
}
