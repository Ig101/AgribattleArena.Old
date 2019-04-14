using AgribattleArena.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Helpers.DelegateLists
{
    class TileActions
    {
        public delegate void Action(ISceneParentRef parent, Tile tile, float time);
    }
}
