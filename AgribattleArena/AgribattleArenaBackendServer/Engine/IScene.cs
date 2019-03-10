using AgribattleArenaBackendServer.Engine.Synchronization;
using AgribattleArenaBackendServer.Models.Battle;
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
        IEnumerable<BattleUserDto> PlayerIds { get; }
        int RandomCounter { get; }

        SynchronizationObject GetSynchronizationData();
        FullSynchronizationObject GetFullSynchronizationData();
        bool DecorationCast(ActiveDecoration actor);
        bool ActorMove(Actor actor, Tile target);
        bool ActorCast(Actor actor, int id, Tile target);
        bool ActorAttack(Actor actor, Tile target);
        bool ActorWait(Actor actor);
    }
}
