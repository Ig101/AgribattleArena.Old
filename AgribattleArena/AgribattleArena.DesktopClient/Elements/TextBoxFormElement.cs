using Ignitus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AgribattleArena.DesktopClient.Elements
{
    class TextBoxFormElement: HudElement
    {
        Game1Shell game;
        string modeName;

        TextBox[] textBoxes;

        string spriteName;
        string cursorSpriteName;
        string font;
        Color textColor;
        Color color;
        int sourceSide;
        int textShift;
        Rectangle cursorSource;

        float cursorTick;
        float remainedTickTime;
        bool tick;

        int? activeTextbox;

        float stepInterval;
        int elementPos;

        public TextBox TempTextbox { get { return activeTextbox == null ? null : textBoxes[activeTextbox.Value]; } }
        public TextBox[] TextBoxes { get { return textBoxes; } set { textBoxes = value; } }
        public float StepInterval { get { return stepInterval; } set { stepInterval = value; } }
        public int? ActiveTextbox { get { return activeTextbox; } set { activeTextbox = value; } }
        public string CursorSpriteName { get { return cursorSpriteName; } set { cursorSpriteName = value; } }
        public string Font { get { return font; } set { font = value; } }
        public Color TextColor { get { return textColor; } set { textColor = value; } }
        public string SpriteName { get { return spriteName; } set { spriteName = value; } }
        public Color Color { get { return color; } set { color = value; } }
        public int SourceSide { get { return sourceSide; } set { sourceSide = value; } }
        public int TextShift { get { return textShift; } set { textShift = value; } }
        public Rectangle CursorSource { get { return cursorSource; } set { cursorSource = value; } }

        public TextBoxFormElement(string name, int x, int y, TextBox[] textBoxes, string spriteName, Color color,
            string font, string cursorSpriteName, int textShift, Color textColor, int sourceSide, Rectangle cursorSource, Game1Shell game, string modeName,
            bool ignoreAnimation, bool ignoreBackAnimation, GameWindow window, int elementPos) 
            :base(name, x, y, -1, -1, false, ignoreAnimation, ignoreBackAnimation)
        {
            this.textBoxes = textBoxes;
            this.game = game;
            this.modeName = modeName;
            window.TextInput += this.OnTextInput;
            tick = false;
            cursorTick = 500f;
            remainedTickTime = cursorTick;
            this.textShift = textShift;
            this.cursorSource = cursorSource;
            this.spriteName = spriteName;
            this.color = color;
            this.sourceSide = sourceSide;
            this.font = font;
            this.cursorSpriteName = cursorSpriteName;
            this.textColor = textColor;
            activeTextbox = 0;
            this.elementPos = elementPos;
        }

        void SetStepInterval()
        {
            stepInterval = 200f;
        }

        public void OnTextInput (object sender, TextInputEventArgs e)
        {
            if (activeTextbox!=null)
            {
                Mode tempMode = game.GetTempMode();
                if (tempMode == game.GetMode(modeName))
                {
                    tempMode.TempElement = elementPos;
                    bool regexMatch = Regex.Match(e.Character.ToString(), TempTextbox.RegExp).Success;
                    if (TempTextbox.MaxLength > TempTextbox.Text.Length && regexMatch)
                    {
                        TempTextbox.Text.Insert(TempTextbox.CursorPosition, e.Character);
                        TempTextbox.CursorPosition++;
                    }
                }
                else
                {
                    activeTextbox = null;
                }
            }
        }

        public override void PassiveUpdate(IgnitusGame game, Mode mode, ControlsState state, ControlsState prevState, float milliseconds)
        {
            int tempElement = 0;
            for (int r = 0; r < mode.Elements.Length; r++)
            {
                if (mode.Elements[r] == this)
                {
                    tempElement = r;
                    break;
                }
            }
            if (state.KeysState[5] && mode.TempElement!=tempElement)
            {
                this.activeTextbox = null;
            }
            Point correctedMousePos = base.TransformPointToElementCoords(state.MousePosition);
            if (state.LeftButtonState && !prevState.LeftButtonState)
            {
                for (int i = 0; i < textBoxes.Length; i++)
                {
                    if (correctedMousePos.X <= textBoxes[i].Position.X + textBoxes[i].Width + 5 &&
                        correctedMousePos.X >= textBoxes[i].Position.X - 5 &&
                        correctedMousePos.Y <= textBoxes[i].Position.Y + textBoxes[i].Height + 5 &&
                        correctedMousePos.Y >= textBoxes[i].Position.Y - 5)
                    {
                        activeTextbox = i;
                        mode.TempElement = tempElement;
                        string str = textBoxes[i].Text.ToString();
                        textBoxes[i].CursorPosition = 0;
                        for(int j = 1; j<=str.Length;j++)
                        {
                            if (game.CalculateStringLength(str.Substring(0, j), font) <= correctedMousePos.X - textBoxes[i].Position.X - TextShift)
                                textBoxes[i].CursorPosition = j;
                        }
                        return;
                    }
                }
            }
            if (stepInterval>=0)stepInterval-=milliseconds;
            if (activeTextbox !=null)
            {
                TextBox tempTextbox = TempTextbox;
                remainedTickTime -= milliseconds;
                if (remainedTickTime < 0)
                {
                    tick = !tick;
                    remainedTickTime = cursorTick;
                }
                if ((stepInterval <= 0 && state.KeysState[2]) || (state.KeysState[2] && !prevState.KeysState[2]))
                {
                    mode.TempElement = elementPos;
                    if (tempTextbox.CursorPosition > 0)
                    {
                        tempTextbox.CursorPosition--;
                        SetStepInterval();
                    }
                }
                if ((stepInterval <= 0 && state.KeysState[3]) || (state.KeysState[3] && !prevState.KeysState[3]))
                {
                    mode.TempElement = elementPos;
                    if (tempTextbox.CursorPosition < tempTextbox.Text.Length)
                    {
                        tempTextbox.CursorPosition++;
                        SetStepInterval();
                    }
                }
                if ((stepInterval <= 0 && state.KeysState[6]) || (state.KeysState[6] && !prevState.KeysState[6]))
                {
                    mode.TempElement = elementPos;
                    if (tempTextbox.CursorPosition > 0)
                    {
                        tempTextbox.Text.Remove(tempTextbox.CursorPosition - 1, 1);
                        tempTextbox.CursorPosition--;
                        SetStepInterval();
                    }
                }
                if ((stepInterval <= 0 && state.KeysState[7]) || (state.KeysState[7] && !prevState.KeysState[7]))
                {
                    mode.TempElement = elementPos;
                    if (tempTextbox.CursorPosition < tempTextbox.Text.Length)
                    {
                        tempTextbox.Text.Remove(tempTextbox.CursorPosition, 1);
                        SetStepInterval();
                    }
                }
                string str = tempTextbox.Text.ToString();
                if (!TempTextbox.CanMoveAfterBorders && game.CalculateStringLength(str, font) > tempTextbox.Width - TextShift * 2)
                {
                    tempTextbox.Text.Remove(tempTextbox.Text.Length - 1, 1);
                    tempTextbox.CursorPosition--;
                }
            }
        }

        public override void Update(IgnitusGame game, Mode mode, ControlsState state, ControlsState prevState, float milliseconds)
        {
            if(state.KeysState[5] && (state.KeysState[4] || state.KeysState[8]))
            {
                if (activeTextbox == null) activeTextbox = textBoxes.Length-1;
                if (mode.StepInterval <= 0)
                {
                    activeTextbox--;
                    mode.SetStepInterval();
                }
                if (activeTextbox < 0)
                {
                    activeTextbox = null;
                    mode.ZeroStepInterval();
                    ArrowsMechanics(mode, state);
                }
            }
            else if (state.KeysState[5])
            {
                if (activeTextbox == null) activeTextbox = 0;
                if (mode.StepInterval <= 0)
                {
                    activeTextbox++;
                    mode.SetStepInterval();
                }
                if (activeTextbox > textBoxes.Length - 1)
                {
                    activeTextbox = null;
                    mode.ZeroStepInterval();
                    ArrowsMechanics(mode, state);
                }
            }
        }

        public override void Draw(IgnitusGame game, Matrix animation, Microsoft.Xna.Framework.Color fonColor, float milliseconds)
        {
            Color color = this.color;
            color.R = (byte)(color.R * (float)fonColor.R / 255);
            color.G = (byte)(color.G * (float)fonColor.G / 255);
            color.B = (byte)(color.B * (float)fonColor.B / 255);
            color.A = (byte)(color.A * (float)fonColor.A / 255);
            foreach (TextBox textBox in textBoxes)
            {
                game.DrawSprite(spriteName, new Rectangle(X + textBox.Position.X, Y + textBox.Position.Y, textBox.Width / 2, textBox.Height), 
                    new Rectangle(0, 0, textBox.Width / textBox.Height * sourceSide / 2, sourceSide),
                    color, 0,
                        new Vector2(0, 0), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
                game.DrawSprite(spriteName, new Rectangle(X + textBox.Position.X + textBox.Width / 2, Y + textBox.Position.Y, textBox.Width / 2, textBox.Height), 
                    new Rectangle(0, 0, textBox.Width / textBox.Height * 64, 128),
                    color, 0,
                    new Vector2(0, 0), Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally, 0);
                string str;
                if (textBox.Password)
                {
                    StringBuilder builder = new StringBuilder();
                    str = builder.Append('*', textBox.Text.Length).ToString();
                }
                else
                {
                    str = textBox.Text.ToString();
                }
                game.DrawString(font, str, true, new Point(X + textBox.Position.X + textShift, Y + textBox.Position.Y + textShift), 10000, textColor);
            }
            if (TempTextbox != null && tick)
            {
                TextBox textBox = TempTextbox;
                string str;
                if (textBox.Password)
                {
                    StringBuilder builder = new StringBuilder();
                    str = builder.Append('*', textBox.Text.Length).ToString();
                }
                else
                {
                    str = textBox.Text.ToString();
                }
                game.DrawSprite(cursorSpriteName, new Rectangle(X + textBox.Position.X + textShift + 10 +
                    game.CalculateStringLength(str.Substring(0, TempTextbox.CursorPosition), font), Y + textBox.Position.Y + textShift,
                    game.CalculateStringLength("I", font) / 8, textBox.Height - textShift * 2), cursorSource, textColor, 0, Vector2.Zero,
                    Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
            }
        }

        public override void DrawPreActionsUpdate(IgnitusGame game, Microsoft.Xna.Framework.Color fonColor)
        {

        }

        public override string ToString()
        {
            return "TextBoxElement";
        }
    }
}
