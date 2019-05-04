using Ignitus;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArena.DesktopClient.Elements
{
    class SyncInfoElement : HudElement
    {
        int version;

        public int Version { get { return version; } }

        public SyncInfoElement(string name)
            : base(name, 0, 0, -1, -1, true, true, true)
        {
            version = 0;
        }

        public int GetNextVersion()
        {
            version++;
            if (version == int.MaxValue) version = 0;
            return version;
        }

        public override void Draw(IgnitusGame game, Matrix animation, Color fonColor, float milliseconds)
        {

        }

        public override void DrawPreActionsUpdate(IgnitusGame game, Color fonColor)
        {

        }

        public override void PassiveUpdate(IgnitusGame game, Mode mode, ControlsState state, ControlsState prevState, float milliseconds)
        {

        }

        public override void Update(IgnitusGame game, Mode mode, ControlsState state, ControlsState prevState, float milliseconds)
        {
        }
    }
}
