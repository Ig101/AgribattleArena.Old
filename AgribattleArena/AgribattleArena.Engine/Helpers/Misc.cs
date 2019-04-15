﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Helpers
{
    public enum PlayerStatus { Playing, Victorious, Defeated }
    public enum Action { Move, Attack, Cast, Wait, Decoration, EndTurn, EndGame }
    public enum ActorAction { Move, Attack, Cast, Wait }

    public static class Misc
    {
        public static float RangeBetween(float x1, float y1, float x2, float y2)
        {
            return (float)Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }
    }
}
