using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Natives;
using AgribattleArena.Engine.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Objects
{
    public class Tile: ITileParentRef
    {
        ISceneParentRef parent;

        TileObject tempObject;
        float height;
        int x;
        int y;
        TileNative native;
        bool affected;

        public bool Affected { get { return affected; } set { affected = value; } }
        public ISceneParentRef Parent { get { return parent; } }
        public TileObject TempObject { get { return tempObject; } set { tempObject = value; } }
        public float Height { get { return height; } set { height = value; } }
        public int X { get { return x; } }
        public int Y { get { return y; } }
        public TileNative Native { get { return native; } set { native = value; } }
        public PointF Center { get
            {
                int tileSize = parent.VarManager.TileSize;
                return new PointF(x, y) * (tileSize + 0.5f);
            }
        }

        public Tile(ISceneParentRef parent, int x, int y, TileNative native, int? height)
        {
            this.parent = parent;
            this.x = x;
            this.y = y;
            this.height = height ?? native.DefaultHeight;
            this.native = native;
            this.affected = true;
        }

        public void Update(float time)
        {
            native.Action?.Invoke(parent, this, time);
        }
    }
}
