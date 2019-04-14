using AgribattleArena.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Helpers.DelegateLists
{
    class SpecEffectActions
    {
        public delegate void Action(ISceneParentRef parent, SpecEffect effect, float time);
    }
}
