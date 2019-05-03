using Ignitus;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArena.DesktopClient.Helpers
{
    public static class ModeHelper
    {
        public static MatrixColorCombo SizeGlow(IgnitusGame game, float animationProgress, Color color)
        {
            return new MatrixColorCombo(Matrix.CreateScale(animationProgress), color);
            /*return new MatrixColorCombo(new Matrix(), Color.Multiply(color, animationProgress));*/
        }

        public static MatrixColorCombo FromAboveGlow(IgnitusGame game, float animationProgress, Color color)
        {
            return new MatrixColorCombo(Matrix.CreateTranslation(new Vector3(0,(-(1-animationProgress)*500),0)), color);
            /*return new MatrixColorCombo(new Matrix(), Color.Multiply(color, animationProgress));*/
        }
    }
}
