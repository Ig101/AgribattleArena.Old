using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine
{
    public interface IPlayerParentRef
    {
        ISceneParentRef Parent { get; }
        int Id { get; }
        List<Actor> KeyActors { get; }
        int TurnsSkipped { get; }
        PlayerStatus Status { get; }
    }
}
