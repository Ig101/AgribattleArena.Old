using AgribattleArena.Engine.Objects;
using AgribattleArena.Engine.Objects.Immaterial.Buffs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Helpers.DelegateLists
{
    class BuffActions
    {
        public delegate void Action(ISceneParentRef scene, IActorParentRef actor, Buff buff, float time);
        public delegate void Applier(IBuffManagerParentRef manager, Buff buff);
    }
}
