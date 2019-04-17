using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.VarManagers
{
    public class VarManager : IVarManager, ForExternalUse.IVarManager
    {
        public float TurnTimeLimit => throw new NotImplementedException();

        public float TurnTimeLimitAfterSkip => throw new NotImplementedException();

        public int SkippedTurnsLimit => throw new NotImplementedException();

        public int TileSize => throw new NotImplementedException();

        public int MaxActionPoints => throw new NotImplementedException();

        public int ConstitutionMod => throw new NotImplementedException();

        public float WillpowerMod => throw new NotImplementedException();

        public float StrengthMod => throw new NotImplementedException();

        public float SpeedMod => throw new NotImplementedException();
    }
}
