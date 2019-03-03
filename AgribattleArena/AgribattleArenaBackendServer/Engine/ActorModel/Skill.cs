using AgribattleArenaBackendServer.Engine.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.ActorModel
{
    public class Skill
    {
        RoleModel roleModel;

        SkillNative native;
        float cd;
        float mod;
        int cost;
        int range;

        float preparationTime;

        public int Range { get { return range + (range>1?roleModel.BuffManager.SkillRange:0); } }
        public SkillNative Native { get { return native; } }
        public float Cd { get { return cd + (cd>0?roleModel.BuffManager.SkillCd:0); } }
        public float Mod { get { return mod; } }
        public int Cost { get { return cost + roleModel.BuffManager.SkillCost; } }
        public float PreparationTime { get { return preparationTime; } set { preparationTime = value; } }

        public Skill (RoleModel roleModel, SkillNative skill, float? cd, float? mod, int? cost, int? range)
        {
            this.range = range ?? skill.DefaultRange;
            this.mod = mod ?? skill.DefaultMod;
            this.cost = cost ?? skill.DefaultCost;
            this.cd = cd ?? skill.DefaultCd;
            this.native = skill;
            this.roleModel = roleModel;
        }

        public void Update(float time)
        {
            if (preparationTime > 0) preparationTime -= time;
        }

        public bool Cast(Tile target)
        {
            if (roleModel.ActionPoints >= cost && preparationTime <= 0 && roleModel.BuffManager.CanAct &&
                Misc.RangeBetween(roleModel.Owner.TempTile.X, roleModel.Owner.TempTile.Y, target.X, target.Y) <= range)
            {
                roleModel.ActionPoints -= cost;
                preparationTime = cd;
                native.Action(roleModel.Owner.Parent, roleModel.Owner, target, this);
                return true;
            }
            return false;
        }

        public float CalculateMod(string[] targetTags)
        {
            float tempMod = this.mod;
            foreach (string attackTag in native.Tags)
            {
                foreach (string defenceTag in targetTags)
                {
                    foreach (TagSynergy synergy in roleModel.Attack)
                    {
                        if (synergy.SelfTag == attackTag && synergy.TargetTag == defenceTag)
                        {
                            tempMod *= synergy.Mod;
                        }
                    }
                }
            }
            return tempMod;
        }

        public float CalculateModSkillPower(string[] targetTags)
        {
            return CalculateMod(targetTags) * roleModel.SkillPower;
        }

        public float CalculateModAttackPower(string[] targetTags)
        {
            return CalculateMod(targetTags) * roleModel.AttackPower;
        }
    }
}
