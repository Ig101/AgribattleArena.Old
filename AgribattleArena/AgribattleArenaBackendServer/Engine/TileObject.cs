using AgribattleArenaBackendServer.Engine.ActorModel;
using AgribattleArenaBackendServer.Engine.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine
{
    public abstract class TileObject : GameObject
    {
        TaggingNative native;
        Tile tempTile;
        DamageModel damageModel;
        float initiativePosition;
        bool affected;

        public bool Affected { get { return affected; } set { affected = value; } }
        public TaggingNative Native { get { return native; } }
        public float InitiativePosition { get { return initiativePosition; } set { initiativePosition = value; } }
        public DamageModel DamageModel { get { return damageModel; } }
        public Tile TempTile { get { return tempTile; }  set { tempTile = value; } }

        public TileObject(Scene parent, Tile tempTile, float z, DamageModel damageModel, TaggingNative native)
            : base(parent, tempTile.GetCenter().X, tempTile.GetCenter().Y, z)
        {
            this.native = native;
            this.tempTile = tempTile;
            this.tempTile.TempObject = this;
            this.damageModel = damageModel;
            this.initiativePosition += (parent.GetNextRandom() / 50);
        }

        public bool Damage (float amount, string[] tags)
        {
            this.Affected = true;
            this.IsAlive = this.IsAlive && !damageModel.Damage(amount, tags);
            return !this.IsAlive;
        }

        public abstract void EndTurn();

        public abstract void StartTurn();
    }
}
