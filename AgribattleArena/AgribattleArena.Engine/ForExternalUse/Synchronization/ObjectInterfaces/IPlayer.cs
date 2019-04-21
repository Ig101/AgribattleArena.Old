﻿using AgribattleArena.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.ForExternalUse.Synchronization.ObjectInterfaces
{
    public interface IPlayer
    {
        int Id { get; }
        List<int?> KeyActorsSync { get; }
        int TurnsSkipped { get; }
        PlayerStatus Status { get; }
    }
}
