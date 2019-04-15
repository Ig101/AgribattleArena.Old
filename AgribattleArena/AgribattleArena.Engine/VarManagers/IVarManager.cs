using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.VarManagers
{
    public interface IVarManager
    {
        int TileSize { get; }

        int SkippedTurnsLimit { get; }
        int MaxActionPoints { get; }
        int ConstitutionMod { get; }
        float WillpowerMod { get; }
        float StrengthMod { get; }
        float SpeedMod { get; }
    }
}
