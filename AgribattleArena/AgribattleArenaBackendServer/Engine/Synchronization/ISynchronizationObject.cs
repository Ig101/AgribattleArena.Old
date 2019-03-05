using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Synchronization
{
    public interface ISynchronizationObject
    {
        int RandomCounter { get; }
        List<TileObject> ChangedActors { get; }
        List<SpecEffect> ChangedEffects { get; }
        List<TileObject> DeletedActors { get; }
        List<SpecEffect> DeletedEffects { get; }
        List<Tile> ChangedTiles { get; }
        Tile[,] TileSet { get; }
    }
}
