using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.ForExternalUse
{
    public interface IScene
    {
        int Version { get; }
        IEnumerable<int> PlayerIds { get; }

        bool ActorMove(int actorId, int targetX, int targetY);
        bool ActorCast(int actorId, int skillId, int targetX, int targetY);
        bool ActorAttack(int actorId, int targetX, int targetY);
        bool ActorWait(int actorId);
    }
}
