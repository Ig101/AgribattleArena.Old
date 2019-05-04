using System;
using System.IO;
using System.Text;
using AgribattleArena.DesktopClient.Elements;
using AgribattleArena.DesktopClient.Helpers;
using Ignitus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Sockets;
using AgribattleArena.DesktopClient.Models;
using AgribattleArena.DesktopClient.ExternalModels.Profile;
using System.Threading.Tasks;
using AgribattleArena.DesktopClient.ExternalCall;

namespace AgribattleArena.DesktopClient
{
    public class Game1Shell : IgnitusGame
    {
        ExternalCallManager externalCallManager;

        const int loadingTime = 1000;

        Color c_color = new Color(255, 255, 255);
        Color c_selected_color = new Color(255, 243, 113, 255);
        Color c_pressed_color = new Color(255, 243, 0, 255);
        string profileFilePath;
        string loginCookie;
        bool savePassword;

        Queue<ExternalCallbackTask> callbackQueue = new Queue<ExternalCallbackTask>();
        Queue<Mode> goToModeQueue = new Queue<Mode>();

        public ExternalCallManager ExternalCallManager { get { return externalCallManager; } }
        public string LoginCookie { get { return loginCookie; } set { loginCookie = value; externalCallManager.SetAuthorizeCookie(value); } }

        public Game1Shell()
            : base(new Vector2(0, 0), 64, 0, new Point(2560, 1600))
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Agribattle";
            profileFilePath = Environment.CurrentDirectory;
            loginCookie = null;
            externalCallManager = new ExternalCallManager(callbackQueue, "https://localhost:444");
        }

        public override void GoToMode(Mode mode)
        {
            goToModeQueue.Enqueue(mode);
        }

        protected override void Initialize()
        {
            base.Initialize();
            modes.Add("loadingScreen", new Mode(null, new HudElement[]{
                    new SpriteElement("loadingScreen", -160,-100,2880,1800,"loadingScreen",Color.White,new Rectangle(0,0,1415,849),
                        false,false),
                    new LabelElement("loading_name",40,1420,2560,"loading",true,false,new Color(255,243,113),"largeFont",false,false),
                    new LoadingWheelElement("loading", 2400, 1460, 200, 200, "loadingWheel", loadingTime, true, Color.White, 1,
                        Game1Shell.PreLoadingMethodBeforeStart, Game1Shell.PreLoadingMethodBeforeStart, false, false),
                }, 5, "loadingScreen",
                Mode.BlackGlow, null, false));
            List<Keys> textBoxKeys = ((Keys[])Enum.GetValues(typeof(Keys))).ToList();
            for (int i = 0; i < textBoxKeys.Count; i++)
            {
                char ch;
                if (!char.TryParse(textBoxKeys[i].ToString(), out ch))
                {
                    textBoxKeys.RemoveAt(i);
                    i--;
                }
            }
            keys = new Keys[9];
            keys[0] = Keys.Enter;
            keys[1] = Keys.Escape;
            keys[2] = Keys.Left;
            keys[3] = Keys.Right;
            keys[4] = Keys.LeftShift;
            keys[5] = Keys.Tab;
            keys[6] = Keys.Back;
            keys[7] = Keys.Delete;
            keys[8] = Keys.RightShift;
            GoToLoadingMode(new object[] { this }, PreLoadingMethodBeforeStart, LoadingMethodBeforeStart, "authorize");
        }

        #region CallbackOperations
        void CallbackQueueProcess()
        {
            if(callbackQueue.Count>0)
            {
                var task = callbackQueue.Dequeue();
                task.CallbackMethod(this, task.Result);
            }
        }
        #endregion

        #region ActionHelpers
        public void ProcessMainInfo(ProfileDto profile)
        {
            Mode main = (Mode)modes["main"];
        }

        public void SaveProfile(string login, string password)
        {
            if (savePassword)
            {
                string str = login + "\n" + password;
                Magic.Act(profileFilePath + @"\profile.mrc", Encoding.UTF8.GetBytes(str));
            }
        }
        #endregion

        #region GameInfo
        protected override void Update(GameTime gameTime)
        {
            if(goToModeQueue.Count>0 && (tempMode == null || tempMode.AnimationProgress >= 1))
            {
                base.GoToMode(goToModeQueue.Dequeue());
            }
            CallbackQueueProcess();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }


        protected override void LoadConfigFromFile(StreamReader reader)
        {
            if (reader.EndOfStream)
            {
                return;
            }
            string[] line = reader.ReadLine().Split(new char[] { '=' });
            if (line.Length > 1 && int.TryParse(line[1], out int result))
            {
                resolution = (Resolution)result;
            }
            if (reader.EndOfStream)
            {
                return;
            }
            line = reader.ReadLine().Split(new char[] { '=' });
            if (line.Length > 1 && int.TryParse(line[1], out result))
            {
                winResolution = (Resolution)result;
            }
            if (reader.EndOfStream)
            {
                return;
            }
            line = reader.ReadLine().Split(new char[] { '=' });
            if (line.Length > 1 && int.TryParse(line[1], out result))
            {
                fullScreen = result == 1;
            }
            if (reader.EndOfStream)
            {
                return;
            }
            line = reader.ReadLine().Split(new char[] { '=' });
            if (line.Length > 1 && int.TryParse(line[1], out result))
            {
                volume = result;
                soundVolume = result;
            }
            if (reader.EndOfStream)
            {
                return;
            }
            line = reader.ReadLine().Split(new char[] { '=' });
            if (line.Length > 1)
            {
                languageName = line[1];
            }
            if (reader.EndOfStream)
            {
                return;
            }
            line = reader.ReadLine().Split(new char[] { '=' });
            if (line.Length > 1 && int.TryParse(line[1], out result))
            {
                savePassword = result == 1;
            }
        }

        protected override object LoadNativeParser(string type, string name, string[] parametres)
        {
            return null;
        }

        protected override void LoadProfile()
        {
            string path = profileFilePath + @"\profile.mrc";
            if (File.Exists(path))
            {
                byte[] bytes = Magic.Restore(profileFilePath + @"\profile.mrc");
                string str = Encoding.UTF8.GetString(bytes);
                string[] strs = str.Split(new char[] { '\n' });
                if (strs.Length == 2 && savePassword)
                {
                    var authResult = (AuthorizeResultDto)externalCallManager.Authorize(new AuthorizeTaskDto(){
                        Login = strs[0],
                        Password = strs[1]
                        });
                    if (authResult.Error == null)
                    {
                        LoginCookie = authResult.Cookie;
                        var profileResult = (GetProfileResultDto)externalCallManager
                            .GetProfileInfo(new GetProfileTaskDto() { Cookie = loginCookie });
                        if (profileResult.Error == null)
                        {
                            ProcessMainInfo(profileResult.Profile);
                            Mode mode = (Mode)modes["loadingScreen"];
                            ((LoadingWheelElement)(mode).Elements[mode.Elements.Length - 1]).TargetMode = "main";
                            return;
                        }
                    }
                }
                File.Delete(path);
            }
        }

        protected override void LoadStartConfig()
        {
            winResolution = Resolution.R800x480;
            resolution = Resolution.RYours;
            volume = 50;
            soundVolume = 50;
            languageName = "eng";
            fullScreen = false;
            savePassword = true;
        }

        protected override bool SaveConfigCore(StreamWriter writer)
        {
            writer.WriteLine("resolution=" + ((int)resolution).ToString());
            writer.WriteLine("winResolution=" + ((int)winResolution).ToString());
            writer.WriteLine("fullscreen=" + (fullScreen ? "1" : "0"));
            writer.WriteLine("volume=" + volume.ToString());
            writer.WriteLine("language=" + languageName);
            writer.WriteLine("savePassword=" + (savePassword? "1" : "0"));
            return true;
        }

        protected override void SaveProfile()
        {

        }

        #region loading
        void LoadLoadingContent()
        {
            if (File.Exists(Environment.CurrentDirectory + "\\Content\\" + "large" + ".xnb"))
                content.Add("largeFont", Content.Load<SpriteFont>("large"));
            if (File.Exists(Environment.CurrentDirectory + "\\Content\\" + "lesser" + ".xnb"))
                content.Add("mediumFont", Content.Load<SpriteFont>("lesser"));
            if (File.Exists(Environment.CurrentDirectory + "\\Content\\" + "small" + ".xnb"))
                content.Add("smallFont", Content.Load<SpriteFont>("small"));
            if (File.Exists(Environment.CurrentDirectory + "\\Content\\" + "smallest" + ".xnb"))
                content.Add("systemFont", Content.Load<SpriteFont>("smallest"));
            if (File.Exists(Environment.CurrentDirectory + "\\Content\\loadingScreen.xnb"))
                content.Add("loadingScreen", Content.Load<Texture2D>("loadingScreen"));
            if (File.Exists(Environment.CurrentDirectory + "\\Content\\loadingWheel.xnb"))
                content.Add("loadingWheel", Content.Load<Texture2D>("loadingWheel"));
            if (File.Exists(Environment.CurrentDirectory + "\\Content\\main_cursor.xnb"))
                content.Add("cursor", Content.Load<Texture2D>("main_cursor"));
        }

        public void LoadEngineContent()
        {
            //TODO MenuContent
            content.Add("border", Content.Load<Texture2D>("main\\border"));
            content.Add("pattern", Content.Load<Texture2D>("main\\pattern"));
            content.Add("title", Content.Load<Texture2D>("main\\title"));
            content.Add("message_screen", Content.Load<Texture2D>("main\\message_screen"));
            content.Add("map", Content.Load<Texture2D>("map\\capital_map"));
            content.Add("back_button", Content.Load<Texture2D>("main\\back_button"));
            content.Add("settings_button", Content.Load<Texture2D>("main\\settings_button"));
        }

        public static void PreLoadingMethodBeforeStart(object[] objs)
        {
            Game1Shell game = (Game1Shell)objs[0];
            game.Content.Unload();
            game.content.Clear();
            game.LoadLoadingContent();
        }

        public static void LoadingMethodBeforeStart(object[] objs)
        {
            Game1Shell game = (Game1Shell)objs[0];
            game.LoadEngineContent();
            game.LoadMainContent();
            game.LoadMainInformation();
        }

        protected override void LoadModes()
        {
            modes.Add("authorize", new Mode((Mode)modes["loadingScreen"], new HudElement[]{
                new SpriteElement("fon", 755, 780, 1050, 750, "pattern", new Color(0,0,40,240), new Rectangle(0,0,8,8),false,false ),
                new BorderElement("border", 745,770,1070,770, "border", new Color(40,40,140), 0.5f, false,false),
                new LabelElement("login", 805, 830, 950, "login", true, false, new Color(255,255,100), "mediumFont", false, false),
                new LabelElement("pass", 805, 1030, 950, "password", true, false, new Color(255,255,100), "mediumFont", false, false),
                new TextBoxFormElement("authorization_field", 805, 910, new TextBox[]{
                    new TextBox(0,0,"login",950,100, false, "[A-Za-z0-9]", 24, false),
                    new TextBox(0,200,"pass",950,100,true, "[a-zA-Z0-9!@#$%^&*()_+-=|\\}{\":; '?.>,<]", 24, false)
                    }, "pattern", new Color(40,40,80,240), "mediumFont", "pattern", 20, new Color(200,200,200), 8, new Rectangle(0,0,8,8),this,
                    "authorize",false,false,Window, 4),
                new ButtonElement("login", 620, 1250, 1320, 100, Id2Str("authorize"), "largeFont", false, c_color, c_selected_color, c_pressed_color,
                ActionsHelper.Authorize,false,false),
                new ButtonElement("register", 620, 1390, 1320, 70, Id2Str("register"), "mediumFont", false, c_color, c_selected_color, c_pressed_color,
                ActionsHelper.GoToRegister,false,false),
                new SpriteButtonElement("exit", 20, 1460, 120, 120, "", "largeFont", Color.White,
                    new Color(175,175,175), new Color(100,100,100),
                    Color.White, "back_button", "back_button", "back_button", new Rectangle(0,0,128,128), ActionsHelper.Exit, true,true),
                new SpriteButtonElement("settings", 150, 1460, 120, 120, "", "largeFont", Color.White,
                    new Color(175,175,175), new Color(100,100,100),
                    Color.White, "settings_button", "settings_button", "settings_button", new Rectangle(0,0,128,128), ActionsHelper.GoToSettings, true,true)
                }, 5, "authorize", ModeHelper.FromAboveGlow, null, true));
            modes.Add("register", new Mode((Mode)modes["loadingScreen"], new HudElement[]{
                new KeyElement("escape", ActionsHelper.GoToAuth,1),
                new SpriteElement("fon", 755, 550, 1050, 1050, "pattern", new Color(0,0,40,240), new Rectangle(0,0,8,8),false,false ),
                new BorderElement("border", 745,540,1070,1070, "border", new Color(40,40,140), 0.5f, false,false),
                new LabelElement("login", 805, 600, 950, "login", true, false, new Color(255,255,100), "mediumFont", false, false),
                new LabelElement("email", 805, 800, 950, "email", true, false, new Color(255,255,100), "mediumFont", false, false),
                new LabelElement("pass", 805, 1000, 950, "password", true, false, new Color(255,255,100), "mediumFont", false, false),
                new LabelElement("repeat_pass", 805, 1200, 950, "confirm_password", true, false, new Color(255,255,100), "mediumFont", false, false),
                new TextBoxFormElement("authorization_field", 805, 680, new TextBox[]{
                    new TextBox(0,0,"login",950,100, false, "[A-Za-z0-9]", 24, false),
                    new TextBox(0,200,"email",950,100,false, "[a-zA-Z0-9!@#$%^&*()_+-=|\\}{\":; '?.>,<]", 100, true),
                    new TextBox(0,400,"pass",950,100,true, "[a-zA-Z0-9!@#$%^&*()_+-=|\\}{\":; '?.>,<]", 24, false),
                    new TextBox(0,600,"repeat_pass",950,100,true, "[a-zA-Z0-9!@#$%^&*()_+-=|\\}{\":; '?.>,<]", 24, false),
                    }, "pattern", new Color(40,40,80,240), "mediumFont", "pattern", 20, new Color(200,200,200), 8, new Rectangle(0,0,8,8),this,
                    "register",false,false,Window,7),
                new ButtonElement("register", 770, 1430, 510, 100, Id2Str("register"), "largeFont", false, c_color, c_selected_color, c_pressed_color,
                ActionsHelper.Register,false,false),
                new ButtonElement("exit", 1280, 1430, 510, 100, Id2Str("back"), "largeFont", false, c_color, c_selected_color, c_pressed_color,
                ActionsHelper.GoToAuth,false,false),
               new SpriteButtonElement("exit", 20, 1460, 120, 120, "", "largeFont", Color.White,
                    new Color(175,175,175), new Color(100,100,100),
                    Color.White, "back_button", "back_button", "back_button", new Rectangle(0,0,128,128), ActionsHelper.Exit, true,true),
                new SpriteButtonElement("settings", 150, 1460, 120, 120, "", "largeFont", Color.White,
                    new Color(175,175,175), new Color(100,100,100),
                    Color.White, "settings_button", "settings_button", "settings_button", new Rectangle(0,0,128,128), ActionsHelper.GoToSettings, true,true)
            }, 5, "register", ModeHelper.FromAboveGlow, null, true));
            SyncInfoElement authSyncElement = new SyncInfoElement("sync");
            modes.Add("authorize_status", new Mode((Mode)modes["loadingScreen"], new HudElement[]
            {
                new KeyElement("escape", ActionsHelper.GoToAuth,1),
                new SpriteElement("fon", 380, 550, 1800, 320, "pattern", new Color(0,0,40,240), new Rectangle(0,0,8,8),false,false ),
                new BorderElement("border", 370,540,1820,340, "border", new Color(40,40,140), 0.5f, false,false),
                new LabelElement("error", 420, 610, 1720, "error", true, false, new Color(255,255,255), "largeFont", false, false),
                new ButtonElement("exit", 620, 760, 1320, 100, Id2Str("ok"), "mediumFont", false, c_color, c_selected_color, c_pressed_color,
                ActionsHelper.GoToAuth,false,false),
                authSyncElement,
                new SpriteElement("exit", 20, 1460, 120, 120, "back_button", Color.White, new Rectangle(0,0,128,128), true,true),
                new SpriteElement("settings", 150, 1460, 120, 120, "settings_button", Color.White, new Rectangle(0,0,128,128), true,true)
            }, 5, "auth_status", ModeHelper.FromAboveGlow, null, true));
            modes.Add("register_status", new Mode((Mode)modes["loadingScreen"], new HudElement[]
            {
                new KeyElement("escape", ActionsHelper.GoToRegister,1),
                new SpriteElement("fon", 380, 550, 1800, 320, "pattern", new Color(0,0,40,240), new Rectangle(0,0,8,8),false,false ),
                new BorderElement("border", 370,540,1820,340, "border", new Color(40,40,140), 0.5f, false,false),
                new LabelElement("error", 420, 610, 1720, "error", true, false, new Color(255,255,255), "largeFont", false, false),
                new ButtonElement("exit", 620, 760, 1320, 100, Id2Str("ok"), "mediumFont", false, c_color, c_selected_color, c_pressed_color,
                ActionsHelper.GoToRegister,false,false),
                authSyncElement,
                new SpriteElement("exit", 20, 1460, 120, 120, "back_button", Color.White, new Rectangle(0,0,128,128), true,true),
                new SpriteElement("settings", 150, 1460, 120, 120, "settings_button", Color.White, new Rectangle(0,0,128,128), true,true)
         }, 5, "auth_status", ModeHelper.FromAboveGlow, null, true));
            modes.Add("main", new Mode(null, new HudElement[]{
                new SpriteElement("map", 130, 20, 2080, 1560, "map", Color.White, new Rectangle(0,0,2080,1560), false, false),
                new BorderElement("border", 120,10,2100,1580,"border",new Color(210,210,210),1.5f,false,false),
                  }, 5, "main", Mode.BlackGlow, null, false));
        }

        protected override void LoadNatives()
        {
            this.natives.Clear();
            //TODO Natives
        }
        #endregion

        public bool GetCapsState()
        {
            var state = Keyboard.GetState();
            return state.CapsLock ^ (state.IsKeyDown(Keys.LeftShift) || state.IsKeyDown(Keys.RightShift));
            
        }

        public Mode GetMode(string name)
        {
            return (Mode)modes[name];
        }
        #endregion
    }
}
