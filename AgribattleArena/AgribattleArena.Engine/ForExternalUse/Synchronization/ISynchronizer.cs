using AgribattleArena.Engine.ForExternalUse.Synchronization.ObjectInterfaces;
using AgribattleArena.Engine.Objects;
using AgribattleArena.Engine.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.ForExternalUse.Synchronization
{
    public interface ISynchronizer
    {
        int RandomCounter { get; }
        IEnumerable<IPlayer> Players { get; }
        IEnumerable<IActor> ChangedActors { get; }
        IEnumerable<IActiveDecoration> ChangedDecorations { get; }
        IEnumerable<ISpecEffect> ChangedEffects { get; }
        IEnumerable<IActor> DeletedActors { get; }
        IEnumerable<IActiveDecoration> DeletedDecorations { get; }
        IEnumerable<ISpecEffect> DeletedEffects { get; }
        IEnumerable<ITile> ChangedTiles { get; }
        ITile[,] TileSet { get; }
    }
}
