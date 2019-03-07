using AgribattleArenaBackendServer.Engine.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities
{
    public class GenerationObject
    {
        int? owner;
        string native;
        int tileX;
        int tileY;
        string roleNative;

        public int? Owner { get { return owner; } }
        public string RoleNative { get { return roleNative; } }
        public string Native { get { return native; } }
        public int TileX { get { return tileX; } }
        public int TileY { get { return tileY; } }

        public GenerationObject (string native, int? owner, int tileX, int tileY, string roleNative)
        {
            this.owner = owner;
            this.roleNative = roleNative;
            this.native = native;
            this.tileX = tileX;
            this.tileY = tileY;
        }

        public GenerationObject(string native, int? owner, int tileX, int tileY)
        {
            this.owner = owner;
            this.roleNative = null;
            this.native = native;
            this.tileX = tileX;
            this.tileY = tileY;
        }
    }
}
