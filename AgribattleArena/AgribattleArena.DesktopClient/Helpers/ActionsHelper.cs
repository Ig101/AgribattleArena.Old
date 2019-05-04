﻿using AgribattleArena.DesktopClient.Elements;
using AgribattleArena.DesktopClient.ExternalCall;
using AgribattleArena.DesktopClient.Models;
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
            Game1Shell gameShell = (Game1Shell)game;
            TextBoxFormElement form = (TextBoxFormElement)mode.Elements[4];
            Mode targetMode = (Mode)game.Modes["authorize_status"];
            ((LabelElement)(targetMode.Elements[3])).Text = "connecting";
            ((ButtonElement)(targetMode.Elements[4])).Text = game.Id2Str("cancel");
            gameShell.ExternalCallManager.InsertTask(
                gameShell.ExternalCallManager.Authorize, new AuthorizeTaskDto()
                {
                    Login = form.TextBoxes[0].StringText,
                    Password = form.TextBoxes[1].StringText,
                    Version = ((SyncInfoElement)(targetMode.Elements[5])).GetNextVersion()
                }, CallbacksStore.AuthorizeCallbackReceiver);
            game.GoToMode(targetMode);
        }

        public static void Register(IgnitusGame game, Mode mode, HudElement element)
        {
            Game1Shell gameShell = (Game1Shell)game;
            TextBoxFormElement form = (TextBoxFormElement)mode.Elements[7];
            Mode targetMode = (Mode)game.Modes["register_status"];
            ((LabelElement)(targetMode.Elements[3])).Text = "connecting";
            ((ButtonElement)(targetMode.Elements[4])).Visible = false;
            gameShell.ExternalCallManager.InsertTask(
                gameShell.ExternalCallManager.Register, new RegisterTaskDto()
                {
                    Login = form.TextBoxes[0].StringText,
                    Email = form.TextBoxes[1].StringText,
                    Password = form.TextBoxes[2].StringText,
                    ConfirmPassword = form.TextBoxes[3].StringText,
                    Version = ((SyncInfoElement)(targetMode.Elements[5])).GetNextVersion()
                }, CallbacksStore.RegisterCallbackReceiver);
            game.GoToMode(targetMode);
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

        public static void GoToSettings(IgnitusGame game, Mode mode, HudElement element)
        {

        }
    }
}
