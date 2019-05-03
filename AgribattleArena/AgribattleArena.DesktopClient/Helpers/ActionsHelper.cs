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
        private static object TextBoxFormElement(Game1Shell game)
        {
            throw new NotImplementedException();
        }

        public static void GoToRegister(IgnitusGame game, Mode mode, HudElement element)
        {
            game.GoToMode("register");
        }

        public static void Authorize(IgnitusGame game, Mode mode, HudElement element)
        {
            TextBoxFormElement form = (TextBoxFormElement)mode.Elements[4];
            string error;
            bool success = ExternalOperationsHelper.Authorize((Game1Shell)game,form.TextBoxes[0].StringText, form.TextBoxes[1].StringText, out error);
            if (success)
            {
                var profile = ExternalOperationsHelper.GetProfile((Game1Shell)game);
                ((Game1Shell)game).ProcessMainInfo(profile);
                game.GoToMode("main");
            }
            else
            {
                LabelElement errorDescr = (LabelElement)(((Mode)game.Modes["authorize_error"]).Elements[4]);
                errorDescr.Text = error;
                game.GoToMode("authorize_error");
            }
        }

        public static void Register(IgnitusGame game, Mode mode, HudElement element)
        {
            TextBoxFormElement form = (TextBoxFormElement)mode.Elements[7];
            string login = form.TextBoxes[0].StringText;
            string email = form.TextBoxes[1].StringText;
            string pass = form.TextBoxes[2].StringText;
            string pass2 = form.TextBoxes[3].StringText;
            if(pass!=pass2)
            {
                LabelElement errorDescr = (LabelElement)(((Mode)game.Modes["register_error"]).Elements[4]);
                errorDescr.Text = "no_same_passes";
                game.GoToMode("register_error");
                return;
            }
            string error;
            bool success = ExternalOperationsHelper.Register((Game1Shell)game,login, email, pass, out error);
            if (success)
            {
                success = ExternalOperationsHelper.Authorize((Game1Shell)game,form.TextBoxes[0].StringText, form.TextBoxes[2].StringText, out error);
                if (success)
                {
                    var profile = ExternalOperationsHelper.GetProfile((Game1Shell)game);
                    ((Game1Shell)game).ProcessMainInfo(profile);
                    game.GoToMode("main");
                }
                else
                {
                    LabelElement errorDescr = (LabelElement)(((Mode)game.Modes["authorize_error"]).Elements[4]);
                    errorDescr.Text = error;
                    game.GoToMode("authorize_error");
                }
            }
            else
            {
                LabelElement errorDescr = (LabelElement)(((Mode)game.Modes["register_error"]).Elements[4]);
                errorDescr.Text = error;
                game.GoToMode("register_error");
            }
        }

        public static void GoToAuth(IgnitusGame game, Mode mode, HudElement element)
        {
            game.GoToMode("authorize");
        }

        public static void Exit(IgnitusGame game, Mode mode, HudElement element)
        {
            game.SaveConfig();
            game.Exit();
        }
    }
}
