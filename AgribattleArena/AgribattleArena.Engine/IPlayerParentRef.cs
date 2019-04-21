using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine
{
    public interface IPlayerParentRef
    {
        int Id { get; }
        int? Team { get; }
        List<Actor> KeyActors { get; }
        int TurnsSkipped { get; }
        PlayerStatus Status { get; }

        void SkipTurn();
        bool ActThisTurn();
        void Defeat();
        void Victory();
    }
}
