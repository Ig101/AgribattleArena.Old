using AgribattleArena.Engine.ForExternalUse.Synchronization.ObjectInterfaces;
using AgribattleArena.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgribattleArena.Engine.Synchronizers.SynchronizationObjects
{
    class ActiveDecoration: IActiveDecoration
    {
        public int Id { get; }
        public string NativeId { get; }
        public float Mod { get; }
        public float InitiativePosition { get; }
        public float Health { get; }
        public float MaxHealth { get; }
        public List<TagSynergy> Armor { get; }
        public string OwnerId { get; }
        public bool IsAlive { get; }
        public int X { get; }
        public int Y { get; }
        public float Z { get; }

        public ActiveDecoration (Objects.ActiveDecoration decoration)
        {
            this.Id = decoration.Id;
            this.NativeId = decoration.Native.IdForFront;
            this.Mod = decoration.Mod;
            this.InitiativePosition = decoration.InitiativePosition;
            this.Health = decoration.DamageModel.Health;
            this.MaxHealth = decoration.DamageModel.MaxHealth;
            this.Armor = new List<TagSynergy>();
            this.Armor.AddRange(decoration.DamageModel.Armor);
            this.OwnerId = decoration.Owner?.Id;
            this.IsAlive = decoration.IsAlive;
            this.X = decoration.X;
            this.Y = decoration.Y;
            this.Z = decoration.Z;
        }
    }
}
