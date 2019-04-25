using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.ForExternalUse.Generation.ObjectInterfaces
{
    public interface IPlayer
    {
        long Id { get; }
        int? Team { get; }
        List<IActor> KeyActorsGen { get; }
    }
}
