using AgribattleArenaBackendServer.Engine.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities
{
    public class GenerationObject
    {
        string native;
        int tileX;
        int tileY;
        string roleNative;

        public string RoleNative { get { return roleNative; } }
        public string Native { get { return native; } }
        public int TileX { get { return tileX; } }
        public int TileY { get { return tileY; } }

        public GenerationObject (string native, int tileX, int tileY, string roleNative)
        {
            this.roleNative = roleNative;
            this.native = native;
            this.tileX = tileX;
            this.tileY = tileY;
        }

        public GenerationObject(string native, int tileX, int tileY)
        {
            this.roleNative = null;
            this.native = native;
            this.tileX = tileX;
            this.tileY = tileY;
        }
    }
}
