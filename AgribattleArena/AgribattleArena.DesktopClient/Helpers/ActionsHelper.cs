using AgribattleArena.DesktopClient.Elements;
using Ignitus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArena.DesktopClient.Helpers
{
    class ActionsHelper
    {
        public static void Authorize(IgnitusGame game, Mode mode, HudElement element)
        {
            TextBoxFormElement form = (TextBoxFormElement)mode.Elements[4];
            bool success = ((Game1Shell)game).Authorize(form.TextBoxes[0].StringText, form.TextBoxes[1].StringText);

        }

        private static object TextBoxFormElement(Game1Shell game)
        {
            throw new NotImplementedException();
        }

        public static void GoToRegister(IgnitusGame game, Mode mode, HudElement element)
        {
            game.GoToMode("register");
        }

        public static void Register(IgnitusGame game, Mode mode, HudElement element)
        {

        }

        public static void GoToAuth(IgnitusGame game, Mode mode, HudElement element)
        {
            game.GoToMode("authorize");
        }
    }
}
