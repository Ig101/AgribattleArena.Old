using AgribattleArenaBackendServer.Engine.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Generator
{
    public class GenerationTile
    {
        int height;
        TileNative native;

        public int Height { get { return height; } }
        public TileNative Native { get { return native; } }

        public GenerationTile(TileNative native, int height)
        {
            this.height = height;
            this.native = native;
        }
    }
}
