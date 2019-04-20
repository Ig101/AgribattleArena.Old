using AgribattleArena.Engine.ForExternalUse;
using AgribattleArena.Engine.ForExternalUse.Synchronization;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Synchronizers
{
    public class SyncEventArgs: EventArgs, ISyncEventArgs
    {
        IScene scene;
        Helpers.Action action;
        int version;
        ISynchronizer syncInfo;

        int? id;
        int? actionId;
        int? targetX;
        int? targetY;

        public Helpers.Action Action { get { return action; } }
        public int Version { get { return version; } }
        public IScene Scene { get { return scene; } }
        public ISynchronizer SyncInfo { get { return syncInfo; } }
        public int? Id { get { return id; } }
        public int? ActionId { get { return actionId; } }
        public int? TargetX { get { return targetX; } }
        public int? TargetY { get { return targetY; } }

        public SyncEventArgs (IScene scene, int version, Helpers.Action action, ISynchronizer syncInfo, int? id, int? actionId, int? targetX, int? targetY )
        {
            this.version = version;
            this.scene = scene;
            this.action = action;
            this.syncInfo = syncInfo;
            this.id = id;
            this.actionId = actionId;
            this.targetX = targetX;
            this.targetY = targetY;
        }
    }
}
