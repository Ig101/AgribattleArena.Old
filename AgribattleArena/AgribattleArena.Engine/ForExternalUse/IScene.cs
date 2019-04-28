using AgribattleArena.Engine.ForExternalUse.Synchronization;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.ForExternalUse
{
    public interface IScene
    {
        float PassedTime { get; }
        int Version { get; }
        bool IsActive { get; }
        IEnumerable<string> PlayerIds { get; }

        ISynchronizer GetFullSynchronizationData();

        bool ActorMove(int actorId, int targetX, int targetY);
        bool ActorCast(int actorId, int skillId, int targetX, int targetY);
        bool ActorAttack(int actorId, int targetX, int targetY);
        bool ActorWait(int actorId);
    }
}
