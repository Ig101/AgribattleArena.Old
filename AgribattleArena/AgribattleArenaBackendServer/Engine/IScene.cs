using AgribattleArenaBackendServer.Engine.Synchronization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine
{
    public interface IScene
    {
        TileObject TempTileObject { get; }
        int Id { get; }
        List<int> PlayerIds { get; }

        SynchronizationObject GetSynchronizationData();
        FullSynchronizationObject GetFullSynchronizationData();
        bool DecorationCast(ActiveDecoration actor);
        bool ActorMove(Actor actor, Tile target);
        bool ActorCast(Actor actor, int id, Tile target);
        bool ActorAttack(Actor actor, Tile target);
        bool ActorWait(Actor actor);
    }
}
