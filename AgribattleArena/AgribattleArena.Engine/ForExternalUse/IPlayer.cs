using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.ForExternalUse
{
    public interface IPlayerShort
    {
        string Id { get; }
        int? Team { get; }
    }
}
