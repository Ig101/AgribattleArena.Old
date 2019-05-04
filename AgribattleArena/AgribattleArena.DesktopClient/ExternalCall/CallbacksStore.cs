using AgribattleArena.DesktopClient.Elements;
using AgribattleArena.DesktopClient.Models;
using Ignitus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArena.DesktopClient.ExternalCall
{
    public static class CallbacksStore
    {
        static void SetupAuthError(Game1Shell game, string error, Mode authorizeMode, Mode registerMode)
        {
            ((LabelElement)(authorizeMode.Elements[3])).Text = error;
            ((LabelElement)(registerMode.Elements[3])).Text = error;
            ((ButtonElement)(authorizeMode.Elements[4])).Text = game.Id2Str("ok");
            ((ButtonElement)(registerMode.Elements[4])).Text = game.Id2Str("ok");
        }

        public static void AuthorizeCallbackReceiver(Game1Shell game, ExternalResultDto result)
        {
            Mode authorizeMode = (Mode)game.Modes["authorize_status"];
            Mode registerMode = (Mode)game.Modes["register_status"];
            if (((SyncInfoElement)(authorizeMode.Elements[5])).Version == result.Version &&
                (game.GetTempMode().Name == "auth_status" || game.GetTargetMode().Name == "auth_status"))
            {
                game.GoToMode("register_status");
                if (result.Error != null)
                {
                    SetupAuthError(game, result.Error, authorizeMode, registerMode);
                    return;
                }
            }
            //var result = resultTask.Result;
            /*   if (result.Error == null)
               {
                   loginCookie = result.Cookie;
                   var profileTask = externalCallManager.GetProfileInfo(loginCookie);
                   profileTask.ContinueWith(this.ProfileInfoAfterAuthorizationCallbackReceiver);
                   profileTask.Start();
               }
               else
               {
                   ((LabelElement)(((Mode)Modes["authorize_status"]).Elements[3])).Text = result.Error;
                   ((LabelElement)(((Mode)Modes["register_status"]).Elements[3])).Text = result.Error;
                   //if(tempMode.Name == "auth_status")
                   //     GoToMode("register_status");
               }*/
        }

        public static void RegisterCallbackReceiver(Game1Shell game, ExternalResultDto result)
        {

        }

        public static void ProfileInfoAfterAuthorizationCallbackReceiver(Game1Shell game, ExternalResultDto result)
        {

        }
    }
}
