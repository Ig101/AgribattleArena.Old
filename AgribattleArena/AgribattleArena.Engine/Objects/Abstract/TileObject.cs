using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Natives;
using AgribattleArena.Engine.Objects.Immaterial;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Objects.Abstract
{
    public abstract class TileObject: GameObject
    {
        TaggingNative native;
        ITileParentRef tempTile;
        DamageModel damageModel;
        float initiativePosition;
        bool affected;

        public bool Affected { get { return affected; } set { affected = value; } }
        public TaggingNative Native { get { return native; } }
        public float InitiativePosition { get { return initiativePosition; } set { initiativePosition = value; } }
        public DamageModel DamageModel { get { return damageModel; } }
        public ITileParentRef TempTile { get { return tempTile; } set { tempTile = value; } }

        public TileObject(ISceneParentRef parent, IPlayerParentRef owner, ITileParentRef tempTile, float z, DamageModel damageModel, TaggingNative native)
            : base(parent, owner, tempTile.Center.X, tempTile.Center.Y, z)
        {
            this.native = native;
            this.tempTile = tempTile;
            this.tempTile.TempObject = this;
            this.damageModel = damageModel;
            this.initiativePosition += (parent.GetNextRandom() / 50);
            this.affected = true;
        }

        public virtual bool Damage(float amount, string[] tags)
        {
            this.Affected = true;
            this.IsAlive = this.IsAlive && !damageModel.Damage(amount, tags);
            return !this.IsAlive;
        }

        public virtual void Kill()
        {
            this.IsAlive = false;
        }

        public void ChangePosition(Tile target, bool changeHeight)
        {
            float heightChange = target.Height - this.TempTile.Height;
            this.Affected = true;
            this.TempTile.Affected = true;
            target.Affected = true;
            target.TempObject = this;
            this.TempTile.TempObject = null;
            this.TempTile = target;
            PointF pos = target.Center;
            this.X = pos.X;
            this.Y = pos.Y;
            this.Z += heightChange;
        }

        public abstract void EndTurn();

        public abstract bool StartTurn();
    }
}
