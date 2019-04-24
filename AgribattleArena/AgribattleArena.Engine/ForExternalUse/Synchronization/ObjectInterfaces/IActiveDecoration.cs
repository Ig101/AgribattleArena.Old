using AgribattleArena.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.ForExternalUse.Synchronization.ObjectInterfaces
{
    public interface IActiveDecoration
    {
        int Id { get; }
        string NativeId { get; }
        float Mod { get; }
        float InitiativePosition { get; }
        float Health { get; }
        int? OwnerId { get; }
        bool IsAlive { get; }
        int X { get; }
        int Y { get; }
        float Z { get; }
        float MaxHealth { get; }
        List<TagSynergy> Armor { get; }
    }
}
