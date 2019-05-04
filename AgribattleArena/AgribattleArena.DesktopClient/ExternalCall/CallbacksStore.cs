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
            ((ButtonElement)(registerMode.Elements[4])).Visible = true;
            registerMode.TempElement = 4;
        }

        public static void AuthorizeCallbackReceiver(Game1Shell game, ExternalResultDto result)
        {
            Mode authorizeMode = (Mode)game.Modes["authorize_status"];
            Mode registerMode = (Mode)game.Modes["register_status"];
            if (((SyncInfoElement)(authorizeMode.Elements[5])).Version == result.Version &&
                (game.GetTempMode().Name == "auth_status" || 
                (game.GetTargetMode()!=null && game.GetTargetMode().Name == "auth_status")))
            {
                if (result.Error != null)
                {
                    SetupAuthError(game, result.Error, authorizeMode, registerMode);
                    return;
                }
                AuthorizeResultDto authResult = (AuthorizeResultDto)result;
                game.LoginCookie = authResult.Cookie;
                game.ExternalCallManager.InsertTask(game.ExternalCallManager.GetProfileInfo, new ExternalTaskDto()
                {
                    Version = result.Version
                }, ProfileInfoAfterAuthorizationCallbackReceiver);
            }
        }

        public static void RegisterCallbackReceiver(Game1Shell game, ExternalResultDto result)
        {
            Mode authorizeMode = (Mode)game.Modes["authorize_status"];
            Mode registerMode = (Mode)game.Modes["register_status"];
            if (((SyncInfoElement)(authorizeMode.Elements[5])).Version == result.Version &&
                (game.GetTempMode().Name == "auth_status" ||
                (game.GetTargetMode() != null && game.GetTargetMode().Name == "auth_status")))
            {
                if (result.Error != null)
                {
                    SetupAuthError(game, result.Error, authorizeMode, registerMode);
                    return;
                }
                RegisterResultDto regResult = (RegisterResultDto)result;
                game.ExternalCallManager.InsertTask(game.ExternalCallManager.Authorize, new AuthorizeTaskDto()
                {
                    Version = result.Version,
                    Login = regResult.Login,
                    Password=regResult.Password
                }, AuthorizeCallbackReceiver);
            }
        }

        public static void ProfileInfoAfterAuthorizationCallbackReceiver(Game1Shell game, ExternalResultDto result)
        {
            Mode authorizeMode = (Mode)game.Modes["authorize_status"];
            Mode registerMode = (Mode)game.Modes["register_status"];
            if (((SyncInfoElement)(authorizeMode.Elements[5])).Version == result.Version &&
                (game.GetTempMode().Name == "auth_status" || game.GetTargetMode().Name == "auth_status"))
            {
                if (result.Error != null)
                {
                    SetupAuthError(game, result.Error, authorizeMode, registerMode);
                    return;
                }
                GetProfileResultDto profileResult = (GetProfileResultDto)result;
                game.ProcessMainInfo(profileResult.Profile);
                game.GoToMode("main");
            }
        }
    }
}
