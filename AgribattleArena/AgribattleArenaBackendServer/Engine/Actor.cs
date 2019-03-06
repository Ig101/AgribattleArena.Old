using AgribattleArenaBackendServer.Engine.ActorModel;
using AgribattleArenaBackendServer.Engine.Helpers;
using AgribattleArenaBackendServer.Engine.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine
{
    public class Actor : TileObject
    {
        RoleModel roleModel;

        public RoleModel RoleModel { get { return roleModel; } }
        public new ActorNative Native { get { return (ActorNative)base.Native; } }

        public Actor(Scene parent, Tile tempTile, float? z, ActorNative native, RoleModelNative roleModel)
            : base(parent, tempTile, z ?? native.DefaultZ, new DamageModel(), native)
        {
            this.roleModel = new RoleModel(this,roleModel);
            this.DamageModel.SetupRoleModel(this.roleModel);
            this.InitiativePosition += 1f / this.roleModel.Initiative;
        }

        public override void Update(float time)
        {
            this.InitiativePosition -= time;
            this.roleModel.Update(time);
        }

        //Actions
        public bool Attack (Tile target)
        {
            return roleModel.AttackingSkill.Cast(target);
        }

        public bool Cast(int id, Tile target)
        {
            Skill skill = roleModel.Skills.Find(x=>x.Id == id); 
            return skill.Cast(target);
        }

        public bool Wait()
        {
            EndTurn();
            return true;
        }

        public bool Move(Tile target)
        {
            if (target.TempObject == null && !target.Native.Unbearable && Math.Abs(target.Height-this.TempTile.Height)<10 && 
                roleModel.BuffManager.CanMove && ((target.X == TempTile.X && Math.Abs(target.Y - TempTile.Y) == 1) ||
                (target.Y == TempTile.Y && Math.Abs(target.X - TempTile.X) == 1)))
            {
                ChangePosition(target);
                return true;
            }
            return false;
        }
        //

        public void ChangePosition (Tile target)
        {
            this.Affected = true;
            this.TempTile.Affected = true;
            target.Affected = true;
            target.TempObject = this;
            this.TempTile.TempObject = null;
            this.TempTile = target;
            Point pos = target.GetCenter();
            this.X = pos.X;
            this.Y = pos.Y;
        }

        public override void EndTurn()
        {
            this.InitiativePosition += 1f/roleModel.Initiative;
            Parent.EndTurn();
        }

        public override void StartTurn()
        {
            this.roleModel.ActionPoints += roleModel.ActionPointsIncome;
        }
    }
}
