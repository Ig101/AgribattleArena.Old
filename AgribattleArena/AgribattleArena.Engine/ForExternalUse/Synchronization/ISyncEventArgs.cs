using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.ForExternalUse.Synchronization
{
    public interface ISyncEventArgs
    {
        Helpers.Action Action { get; }
        int Version { get; }
        IScene Scene { get; }
        ISynchronizer SyncInfo { get; }
        int? Id { get; }
        int? ActionId { get; }
        int? TargetX { get; }
        int? TargetY { get; }
    }
}
