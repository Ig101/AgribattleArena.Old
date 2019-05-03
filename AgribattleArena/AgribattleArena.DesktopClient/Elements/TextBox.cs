using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArena.DesktopClient.Elements
{
    class TextBox
    {
        string name;
        Point position;
        bool password;
        string regExp;
        StringBuilder text;
        int maxLength;
        int cursorPosition;
        int width;
        int height;
        bool canMoveAfterBorders;

        public bool CanMoveAfterBorders { get { return canMoveAfterBorders; } }
        public string Name { get { return name; }}
        public Point Position { get { return position; } set { position = value; } }
        public bool Password { get { return password; } set { password = value; } }
        public string RegExp { get { return regExp; } set { regExp = value; } }
        public StringBuilder Text { get { return text; } set { text = value; } }
        public string StringText { get { return text.ToString(); } set { text.Clear(); text.Append(value); } }
        public int MaxLength { get { return maxLength; } set { maxLength = value; } }
        public int CursorPosition { get { return cursorPosition; } set { cursorPosition = value; } }
        public int Width { get { return width; } set { width = value; } }
        public int Height { get { return height; } set { height = value; } }

        public TextBox(int x, int y, string name, int width, int height, bool password, string regExp, int maxLength, bool canMoveAfterBorders)
        {
            this.canMoveAfterBorders = canMoveAfterBorders;
            this.name = name;
            text = new StringBuilder();
            position = new Point(x, y);
            this.width = width;
            this.height = height;
            this.password = password;
            this.regExp = regExp;
            this.maxLength = maxLength;
        }
    }
}
