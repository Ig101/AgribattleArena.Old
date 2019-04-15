using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine
{
    public interface IPlayerParentRef
    {
        ISceneParentRef Parent { get; }
        int Id { get; }
        List<TileObject> KeyActors { get; }
        int TurnsSkipped { get; }
        PlayerStatus Status { get; }
    }
}
