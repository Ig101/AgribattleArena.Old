using AgribattleArena.Engine.VarManagers;

namespace AgribattleArena.Engine
{
    class Scene
    {
        readonly IVarManager varManager;

        public IVarManager VarManager { get { return varManager; } }
    }
}
