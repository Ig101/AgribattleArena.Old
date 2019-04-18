﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.ForExternalUse.Synchronization.ObjectInterfaces
{
    public interface ISpecEffect
    {
        int? OwnerId { get; }
        bool IsAlive { get; }
        float X { get; }
        float Y { get; }
        float Z { get; }
        float? Duration { get; }
        float Mod { get; }
        string NativeId { get; }
    }
}
