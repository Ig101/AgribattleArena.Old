using AgribattleArena.Engine.VarManagers;

namespace AgribattleArena.Engine
{
    interface ISceneParentRef
    {
        IVarManager VarManager { get; }

        int GetNextId();
    }
}
