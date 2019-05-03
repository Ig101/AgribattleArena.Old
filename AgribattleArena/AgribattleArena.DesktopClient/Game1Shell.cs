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

namespace AgribattleArena.DesktopClient
{
    public class Game1Shell : IgnitusGame
    {
        const int loadingTime = 1000;

        Color c_color = new Color(255, 255, 255);
        Color c_selected_color = new Color(255, 243, 113, 255);
        Color c_pressed_color = new Color(255, 243, 0, 255);
        string profileFilePath;
        string loginCookie;
        bool savePassword;

        public Game1Shell()
            : base(new Vector2(0, 0), 64, 0, new Point(2560, 1600))
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Agribattle";
            profileFilePath = Environment.CurrentDirectory;
            loginCookie = null;
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

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public bool Authorize(string login, string password)
        {
            
            if (loginCookie != null)
            {
                if (savePassword)
                {
                    string str = login + "\n" + password;
                    Magic.Act(profileFilePath + @"\profile.mrc", Encoding.UTF8.GetBytes(str));
                }
                return true;
            }
            return false;
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
                fullScreen = result == 1;
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
                if(strs.Length!=2 || !Authorize(strs[0],strs[1]) || !savePassword)
                {
                    File.Delete(path);
                    return;
                }
                Mode mode = (Mode)modes["loadingScreen"];
                ((LoadingWheelElement)(mode).Elements[mode.Elements.Length-1]).TargetMode = "main";
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
                new SpriteElement("fon", 580, 750, 1400, 610, "pattern", new Color(0,0,40,240), new Rectangle(0,0,8,8),false,false ),
                new BorderElement("border", 570,740,1420,630, "border", new Color(255,243,113), 0.5f, false,false),
                new LabelElement("login", 620, 820, 1000, "login", true, true, new Color(255,255,100), "mediumFont", false, false),
                new LabelElement("pass", 620, 940, 1000, "pass", true, true, new Color(255,255,100), "mediumFont", false, false),
                new TextBoxFormElement("authorization_field", 980, 800, new TextBox[]{
                    new TextBox(0,0,"login",950,100, false, "[A-Za-z0-9]", 24, false),
                    new TextBox(0,120,"pass",950,100,true, "[a-zA-Z0-9!@#$%^&*()_+-=|\\}{\":; '?.>,<]", 24, false)
                    }, "pattern", new Color(40,40,80,240), "mediumFont", "pattern", 20, new Color(200,200,200), 8, new Rectangle(0,0,8,8),this,
                    "authorize",false,false,Window),
                new ButtonElement("login", 620, 1080, 1320, 100, Id2Str("authorize"), "largeFont", false, c_color, c_selected_color, c_pressed_color,
                ActionsHelper.Authorize,false,false),
                new ButtonElement("register", 620, 1220, 1320, 100, Id2Str("register"), "mediumFont", false, c_color, c_selected_color, c_pressed_color,
                ActionsHelper.GoToRegister,false,false)
                }, 5, "authorize", ModeHelper.FromAboveGlow, null, true));
            modes.Add("register", new Mode((Mode)modes["loadingScreen"], new HudElement[]{
                new EscapeElement("escape", ActionsHelper.GoToAuth),
                new SpriteElement("fon", 580, 650, 1400, 830, "pattern", new Color(0,0,40,240), new Rectangle(0,0,8,8),false,false ),
                new BorderElement("border", 570,640,1420,850, "border", new Color(255,243,113), 0.5f, false,false),
                new LabelElement("login", 620, 720, 1000, "login", true, true, new Color(255,255,100), "mediumFont", false, false),
                new LabelElement("email", 620, 840, 1000, "email", true, true, new Color(255,255,100), "mediumFont", false, false),
                new LabelElement("pass", 620, 960, 1000, "pass", true, true, new Color(255,255,100), "mediumFont", false, false),
                new LabelElement("repeat_pass", 620, 1080, 1000, "confirm_pass", true, true, new Color(255,255,100), "mediumFont", false, false),
                new TextBoxFormElement("authorization_field", 980, 700, new TextBox[]{
                    new TextBox(0,0,"login",950,100, false, "[A-Za-z0-9]", 24, false),
                    new TextBox(0,120,"email",950,100,false, "[a-zA-Z0-9!@#$%^&*()_+-=|\\}{\":; '?.>,<]", 100, true),
                    new TextBox(0,240,"pass",950,100,true, "[a-zA-Z0-9!@#$%^&*()_+-=|\\}{\":; '?.>,<]", 24, false),
                    new TextBox(0,360,"repeat_pass",950,100,true, "[a-zA-Z0-9!@#$%^&*()_+-=|\\}{\":; '?.>,<]", 24, false),
                    }, "pattern", new Color(40,40,80,240), "mediumFont", "pattern", 20, new Color(200,200,200), 8, new Rectangle(0,0,8,8),this,
                    "register",false,false,Window),
                new ButtonElement("register", 620, 1200, 1320, 100, Id2Str("register"), "largeFont", false, c_color, c_selected_color, c_pressed_color,
                ActionsHelper.Register,false,false),
                new ButtonElement("exit", 620, 1340, 1320, 100, Id2Str("back"), "mediumFont", false, c_color, c_selected_color, c_pressed_color,
                ActionsHelper.GoToAuth,false,false)
            }, 5, "register", ModeHelper.FromAboveGlow, null, true));
          //  modes.Add("main", new Mode(null, new HudElement[]{
          //      }, 5, "main", Mode.BlackGlow, null, false));
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
    }
}
