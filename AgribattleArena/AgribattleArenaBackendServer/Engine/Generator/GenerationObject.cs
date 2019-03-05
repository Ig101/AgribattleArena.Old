using AgribattleArenaBackendServer.Engine.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Generator
{
    public class GenerationObject
    {
        TaggingNative native;
        int tileX;
        int tileY;
        RoleModelNative roleNative;

        public RoleModelNative RoleNative { get { return roleNative; } }
        public TaggingNative Native { get { return native; } }
        public int TileX { get { return tileX; } }
        public int TileY { get { return tileY; } }

        public GenerationObject (ActorNative native, int tileX, int tileY, RoleModelNative roleNative)
        {
            this.roleNative = roleNative;
            this.native = native;
            this.tileX = tileX;
            this.tileY = tileY;
        }

        public GenerationObject(DecorationNative native, int tileX, int tileY)
        {
            this.roleNative = null;
            this.native = native;
            this.tileX = tileX;
            this.tileY = tileY;
        }
    }
}
