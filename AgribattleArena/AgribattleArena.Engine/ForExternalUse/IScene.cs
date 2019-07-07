using AgribattleArena.Engine.ForExternalUse.Synchronization;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.ForExternalUse
{
    public interface IScene
    {
        float PassedTime { get; }
        string EnemyActorsPrefix { get; }
        int Version { get; }
        bool IsActive { get; }
        IEnumerable<IPlayerShort> ShortPlayers { get; }
        IEnumerable<int> GetPlayerActors(string playerId);

        ISynchronizer GetFullSynchronizationData();

        void UpdateTime(float time);
        bool ActorMove(int actorId, int targetX, int targetY);
        bool ActorCast(int actorId, int skillId, int targetX, int targetY);
        bool ActorAttack(int actorId, int targetX, int targetY);
        bool ActorWait(int actorId);
    }
}
