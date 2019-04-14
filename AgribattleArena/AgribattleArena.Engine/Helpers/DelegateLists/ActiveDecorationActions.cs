using AgribattleArena.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Helpers.DelegateLists
{
    class ActiveDecorationActions
    {
        public delegate void Action(ISceneParentRef scene, ActiveDecoration activeDecoration);
    }
}
