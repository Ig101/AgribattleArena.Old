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
        public static void AuthorizeCallbackReceiver(Game1Shell game, ExternalResultDto resultTask)
        {
            if (game.GetTempMode().Name == "auth_status")
            {
                if (resultTask.Error != null)
                {
                    Mode authorizeMode = (Mode)game.Modes["authorize_status"];
                    Mode registerMode = (Mode)game.Modes["register_status"];
                    ((LabelElement)(authorizeMode.Elements[3])).Text = resultTask.Error;
                    ((LabelElement)(registerMode.Elements[3])).Text += resultTask.Error;
                    ((ButtonElement)(authorizeMode.Elements[4])).Text = game.Id2Str("ok");
                    ((ButtonElement)(registerMode.Elements[4])).Text = game.Id2Str("ok");
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

        public static void ProfileInfoAfterAuthorizationCallbackReceiver(Game1Shell game, ExternalResultDto resultTask)
        {

        }
    }
}
