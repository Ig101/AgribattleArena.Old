using AgribattleArenaBackendServer.Engine.ActorModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities
{
    public class PlayerActor
    {
        int? owner;
        string native;
        RoleModel roleModel;
        int tileX;
        int tileY;

        public int? Owner { get { return owner; } }
        public RoleModel RoleModel { get { return roleModel; } }
        public string Native { get { return native; } }
        public int TileX { get { return tileX; } }
        public int TileY { get { return tileY; } }

        public PlayerActor(string native, int? owner, int tileX, int tileY, RoleModel roleModel)
        {
            this.owner = owner;
            this.roleModel = roleModel;
            this.native = native;
            this.tileX = tileX;
            this.tileY = tileY;
        }
    }
}
