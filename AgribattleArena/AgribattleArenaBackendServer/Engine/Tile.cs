using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgribattleArenaBackendServer.Engine.Natives;

namespace AgribattleArenaBackendServer.Engine
{
    public class Tile
    {
        Scene parent;

        TileObject tempObject;
        int height;

        int x;
        int y;
        TileNative native;

        public Scene Parent { get { return parent; } }
        public TileObject TempObject { get { return tempObject; } set { tempObject = value; } }
        public int Height { get { return height; } set { height = value; } }
        public int X { get { return x; } }
        public int Y { get { return y; } }
        public TileNative Native { get { return native; } set { native = value; } }

        public Tile (Scene parent, int x, int y, TileNative native, int? height)
        {
            this.parent = parent;
            this.x = x;
            this.y = y;
            this.height = height ?? native.DefaultHeight;
            this.native = native;
        }

        public void Update(float time)
        {
            native.Action?.Invoke(parent, this, native.ActionMod, time);
        }

        public Point GetCenter()
        {
            return new Point(0, 0);
        }
    }
}
