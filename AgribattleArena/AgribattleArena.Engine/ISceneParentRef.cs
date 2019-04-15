using AgribattleArena.Engine.VarManagers;

namespace AgribattleArena.Engine
{
    public interface ISceneParentRef
    {
        IVarManager VarManager { get; }

        int GetNextId();
    }
}
