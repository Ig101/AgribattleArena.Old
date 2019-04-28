using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.ForExternalUse.Generation.ObjectInterfaces
{
    public interface IPlayer
    {
        string Id { get; }
        int? Team { get; }
        List<IActor> KeyActorsGen { get; }
    }
}
