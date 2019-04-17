using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Objects
{
    public interface ITileParentRef
    {
        ISceneParentRef Parent { get; }
        PointF Center { get; }
        bool Affected { get; set; }
        float Height { get; set; }
        int X { get; }
        int Y { get; }
        TileObject TempObject { get; set; }
    }
}
