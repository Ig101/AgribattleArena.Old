using Ignitus;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArena.DesktopClient.Elements
{
    class EscapeElement : HudElement
    {
        PressButtonAction action;

        public PressButtonAction Action { get { return action; } set { action = value; } }

        public EscapeElement(string name, PressButtonAction action)
            :base(name,0,0,-1,-1,true,true,true)
        {
            this.action = action;
        }

        public override void Draw(IgnitusGame game, Matrix animation, Color fonColor, float milliseconds)
        {
        }

        public override void DrawPreActionsUpdate(IgnitusGame game, Color fonColor)
        {
        }

        public override void PassiveUpdate(IgnitusGame game, Mode mode, ControlsState state, ControlsState prevState, float milliseconds)
        {
            if(!state.KeysState[1] && prevState.KeysState[1])
            {
                action?.Invoke(game, mode, this);
            }
        }

        public override void Update(IgnitusGame game, Mode mode, ControlsState state, ControlsState prevState, float milliseconds)
        {
        }
    }
}
