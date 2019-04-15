using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.VarManagers
{
    public interface IVarManager
    {
        int SkippedTurnsLimit { get; }
    }
}
