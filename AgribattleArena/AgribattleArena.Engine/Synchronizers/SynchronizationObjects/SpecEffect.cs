using AgribattleArena.Engine.ForExternalUse.Synchronization.ObjectInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Synchronizers.SynchronizationObjects
{
    class SpecEffect: ISpecEffect
    {
        public int Id { get; }
        public int? OwnerId { get; }
        public bool IsAlive { get; }
        public float X { get; }
        public float Y { get; }
        public float Z { get; }
        public float? Duration { get; }
        public float Mod { get; }
        public string NativeId { get; }
        public string SecretNativeId { get; }

        public SpecEffect(Objects.SpecEffect specEffect)
        {
            this.Id = specEffect.Id;
            this.OwnerId = specEffect.Owner?.Id;
            this.IsAlive = specEffect.IsAlive;
            this.X = specEffect.X;
            this.Y = specEffect.Y;
            this.Z = specEffect.Z;
            this.Duration = specEffect.Duration;
            this.Mod = specEffect.Mod;
            this.NativeId = specEffect.Native.IdForFront;
            this.SecretNativeId = specEffect.Native.Id;
        }
    }
}
