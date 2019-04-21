using AgribattleArena.Engine.Objects;
using AgribattleArena.Engine.Objects.Immaterial.Buffs;

namespace AgribattleArena.Engine.Helpers.DelegateLists
{
    public class BuffActions
    {
        public delegate void Action(ISceneParentRef scene, IActorParentRef actor, Buff buff, float time);
        public delegate void Applier(IBuffManagerParentRef manager, Buff buff);
        public delegate void OnPurgeAction(ISceneParentRef scene, IActorParentRef actor, Buff buff);
    }
}
