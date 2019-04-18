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
        public int? TempActorId { get; }
        public float Height { get; }
        public string NativeId { get; }

        public Tile(Objects.Tile tile)
        {
            this.X = tile.X;
            this.Y = tile.Y;
            this.TempActorId = tile.TempObject?.Id;
            this.Height = tile.Height;
            this.NativeId = tile.Native.Id;
        }
    }
}
