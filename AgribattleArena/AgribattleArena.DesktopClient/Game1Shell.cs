using System;
using System.IO;
using System.Text;
using Ignitus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AgribattleArena.DesktopClient
{
    public class Game1Shell : IgnitusGame
    {
        const int loadingTime = 0;

        Color c_color = new Color(200, 200, 200, 200);
        Color c_selected_color = new Color(240, 240, 240, 255);
        Color c_pressed_color = new Color(120, 120, 120, 255);

        string profileFilePath;
        string loginCookie;

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
                        Game1Shell.PreLoadingMethodBeforeStart, Game1Shell.PreLoadingMethodBeforeStart, false, false)
                }, 5, "loadingScreen",
                Mode.BlackGlow, null, false));
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
            //TODO Authorize and save profile
            if (loginCookie != null)
            {
                string str = login + "\n" + password;
                Magic.Act(profileFilePath + @"\profile.mrc", Encoding.UTF8.GetBytes(str));
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
                if(strs.Length!=2 || !Authorize(strs[0],strs[1]))
                {
                    File.Delete(path);
                    return;
                }
                ((LoadingWheelElement)((Mode)modes["loadingScreen"]).Elements[2]).TargetMode = "main";
            }
        }

        protected override void LoadStartConfig()
        {
            winResolution = Resolution.R800x480;
            resolution = Resolution.RYours;
            volume = 50;
            soundVolume = 50;
            languageName = "eng";
            fullScreen = true;
        }

        protected override bool SaveConfigCore(StreamWriter writer)
        {
            writer.WriteLine("resolution=" + ((int)resolution).ToString());
            writer.WriteLine("winResolution=" + ((int)winResolution).ToString());
            writer.WriteLine("fullscreen=" + (fullScreen ? "1" : "0"));
            writer.WriteLine("volume=" + volume.ToString());
            writer.WriteLine("language=" + languageName);
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
            this.modes.Clear();
            //TODO Modes
        }

        protected override void LoadNatives()
        {
            this.natives.Clear();
            //TODO Natives
        }
        #endregion
    }
}
