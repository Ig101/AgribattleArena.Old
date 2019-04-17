using AgribattleArena.Engine.ForExternalUse.Synchronization.ObjectInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Synchronizers.SynchronizationObjects
{
    class Tile: ITile
    {
        public int X { get; }
        public int Y { get; }

        public Tile(Objects.Tile tile)
        {
        }
    }
}
