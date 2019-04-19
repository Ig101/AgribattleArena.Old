﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.VarManagers
{
    public class VarManager : IVarManager, ForExternalUse.IVarManager
    {
        public float TurnTimeLimit { get; }
        public float TurnTimeLimitAfterSkip { get; }
        public int SkippedTurnsLimit { get; }

        public int TileSize { get; }

        public int MaxActionPoints { get; }
        public int ConstitutionMod { get; }
        public float WillpowerMod { get; }
        public float StrengthMod { get; }
        public float SpeedMod { get; }

        public VarManager(float turnTimeLimit, float turnTimeLimitAfterSkip, int skippedTurnsLimit, int tileSize, int maxActionPoints, 
            int constitutionMod, float willpowerMod, float strengthMod, float speedMod)
        {
            this.TurnTimeLimit = turnTimeLimit;
            this.TurnTimeLimitAfterSkip = turnTimeLimitAfterSkip;
            this.SkippedTurnsLimit = skippedTurnsLimit;
            this.TileSize = tileSize;
            this.MaxActionPoints = maxActionPoints;
            this.ConstitutionMod = constitutionMod;
            this.WillpowerMod = willpowerMod;
            this.StrengthMod = strengthMod;
            this.SpeedMod = speedMod;
        }
    }
}
