using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Natives;
using AgribattleArena.Engine.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgribattleArena.Engine.Objects.Immaterial
{
    public class Skill: IdObject
    {
        IActorParentRef parent;

        SkillNative native;
        float cd;
        float mod;
        int cost;
        int range;
        float preparationTime;

        public int Range { get { return range + (range > 1 ? parent.BuffManager.SkillRange : 0); } }
        public SkillNative Native { get { return native; } }
        public float Cd { get { return cd + (cd > 0 ? parent.BuffManager.SkillCd : 0); } }
        public float Mod { get { return mod; } }
        public int Cost { get { return cost + parent.BuffManager.SkillCost; } }
        public float PreparationTime { get { return preparationTime; } set { preparationTime = value; } }
        public IEnumerable<string> AggregatedTags { get { return native.Tags.Concat(parent.Tags); } }

        public Skill(IActorParentRef parent, SkillNative skill, float? cd, float? mod, int? cost, int? range)
            : base(parent.Parent)
        {
            this.range = range ?? skill.DefaultRange;
            this.mod = mod ?? skill.DefaultMod;
            this.cost = cost ?? skill.DefaultCost;
            this.cd = cd ?? skill.DefaultCd;
            this.native = skill;
            this.parent = parent;
        }

        public void Update(float time)
        {
            if (preparationTime > 0) preparationTime -= time;
        }

        public bool Cast(Tile target)
        {
            if (parent.ActionPoints >= cost && preparationTime <= 0 && parent.BuffManager.CanAct &&
                Misc.RangeBetween(parent.X, parent.Y, target.X, target.Y) <= range)
            {
                native.Action(parent.Parent, parent, target, this);
                preparationTime = cd;
                parent.SpendActionPoints(cost);
                return true;
            }
            return false;
        }

        public float CalculateMod(string[] targetTags)
        {
            float tempMod = this.mod;
            foreach (string attackTag in AggregatedTags)
            {
                foreach (string defenceTag in targetTags)
                {
                    foreach (TagSynergy synergy in parent.AttackModifiers)
                    {
                        if ((synergy.SelfTag == attackTag || synergy.SelfTag == null) && (synergy.TargetTag == defenceTag || synergy.TargetTag == null) &&
                            !(synergy.SelfTag == null && synergy.TargetTag == null))
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
            return CalculateMod(targetTags) * parent.SkillPower;
        }

        public float CalculateModAttackPower(string[] targetTags)
        {
            return CalculateMod(targetTags) * parent.AttackPower;
        }
    }
}
