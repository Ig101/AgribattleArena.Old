using AgribattleArenaBackendServer.Models.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Generator.GeneratorEntities
{
    public class PlayerActorWithTile: PlayerActor
    {
        int tileX;
        int tileY;

        public int TileX { get { return tileX; } }
        public int TileY { get { return tileY; } }

        public PlayerActorWithTile(string native, int? owner, int tileX, int tileY, RoleModelNativeToAddDto roleModel)
            :base(native,owner,roleModel)
        {
            this.tileX = tileX;
            this.tileY = tileY;
        }
    }
}
