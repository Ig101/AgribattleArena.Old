using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgribattleArenaBackendServer.Engine.Helpers;
using AgribattleArenaBackendServer.Models.Natives;

namespace AgribattleArenaBackendServer.Engine
{
    public class Tile
    {
        Scene parent;

        TileObject tempObject;
        float height;

        int x;
        int y;
        TileNativeDto native;
        bool affected;

        public bool Affected { get { return affected; } set { affected = value; } }
        public Scene Parent { get { return parent; } }
        public TileObject TempObject { get { return tempObject; } set { tempObject = value; } }
        public float Height { get { return height; } set { height = value; } }
        public int X { get { return x; } }
        public int Y { get { return y; } }
        public TileNativeDto Native { get { return native; } set { native = value; } }

        public Tile (Scene parent, int x, int y, TileNativeDto native, int? height)
        {
            this.parent = parent;
            this.x = x;
            this.y = y;
            this.height = height ?? native.DefaultHeight;
            this.native = native;
        }

        public void Update(float time)
        {
            //native.Action?.Invoke(parent, this, native.ActionMod, time);
            if (Native.Action != null)
            {
                Jint.Engine actionEngine = new Jint.Engine();
                actionEngine
                    .SetValue("scene", Parent)
                    .SetValue("act", this)
                    .SetValue("mod", native.ActionMod)
                    .SetValue("time", time)
                    .Execute(Native.Action.Script);
            }
        }

        public Point GetCenter()
        {
            return new Point(0, 0);
        }
    }
}
