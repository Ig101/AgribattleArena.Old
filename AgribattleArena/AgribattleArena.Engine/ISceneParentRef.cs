using AgribattleArena.Engine.NativeManagers;
using AgribattleArena.Engine.Objects;
using AgribattleArena.Engine.Objects.Abstract;
using AgribattleArena.Engine.VarManagers;

namespace AgribattleArena.Engine
{
    public interface ISceneParentRef
    {
        IVarManager VarManager { get; }
        INativeManager NativeManager { get; }

        TileObject TempTileObject { get; }

        int GetNextId();
        float GetNextRandom();

        void EndTurn();

        bool DecorationCast(ActiveDecoration actor);
        bool ActorMove(Actor actor, Tile target);
        bool ActorCast(Actor actor, int id, Tile target);
        bool ActorAttack(Actor actor, Tile target);
        bool ActorWait(Actor actor);
    }
}
