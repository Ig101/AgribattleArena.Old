using Ignitus;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArena.DesktopClient.Elements
{
    class KeyElement : HudElement
    {
        PressButtonAction action;
        int key;

        public PressButtonAction Action { get { return action; } set { action = value; } }

        public KeyElement(string name, PressButtonAction action, int key)
            :base(name,0,0,-1,-1,true,true,true)
        {
            this.key = key;
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
            if(!state.KeysState[key] && prevState.KeysState[key])
            {
                action?.Invoke(game, mode, this);
            }
        }

        public override void Update(IgnitusGame game, Mode mode, ControlsState state, ControlsState prevState, float milliseconds)
        {
        }
    }
}
