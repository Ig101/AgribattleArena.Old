using AgribattleArena.Engine.Objects;

namespace AgribattleArena.Engine.Helpers.DelegateLists
{
    public class SpecEffectActions
    {
        public delegate void Action(ISceneParentRef parent, SpecEffect effect, float time);
        public delegate void OnDeathAction(ISceneParentRef parent, SpecEffect effect);
    }
}
