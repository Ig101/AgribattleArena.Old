using AgribattleArenaBackendServer.Engine.ActorModel.Buffs;
using AgribattleArenaBackendServer.Engine.Helpers;
using AgribattleArenaBackendServer.Engine.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.ActorModel
{
    public class RoleModel
    {

        BuffManager buffManager;
        Actor owner;

        int strength;
        int willpower;
        int constitution;
        int speed;

        TagSynergy[] armor;
        int actionPointsIncome;

        int actionPoints;
        List<Skill> skills;
        Skill attackingSkill;

        public Skill AttackingSkill { get { return attackingSkill; } }
        public Actor Owner { get { return owner; } }
        public BuffManager BuffManager { get { return buffManager; } }
        public int Strength { get { return strength + buffManager.Strength; } }
        public int Willpower { get { return willpower + buffManager.Willpower; } }
        public int Constitution { get { return constitution + buffManager.Constitution; } }
        public int Speed { get { return speed + buffManager.Speed; } }

        public int MaxHealth { get { return Constitution * Misc.healthPerConst + buffManager.MaxHealth; } }
        public int ActionPointsIncome { get { return actionPointsIncome + buffManager.ActionPointsIncome; } set { actionPointsIncome = value; } }
        public int ActionPoints { get { return actionPoints; } set { actionPoints = value>Misc.maxActionPoints?Misc.maxActionPoints:value; } }
        public float SkillPower { get { return Willpower * Misc.willpowerMod + buffManager.SkillPower; } }
        public float AttackPower { get { return Strength * Misc.strengthMod + buffManager.AttackPower; } }
        public float Initiative { get { return Speed * Misc.speedMod + buffManager.Initiative; } }

        public List<Skill> Skills { get { return skills; } }
        public List<Buff> Buffs { get { return buffManager.Buffs; } }
        public TagSynergy[] DefaultArmor { get { return armor; } }
        public List<TagSynergy> Armor { get { return buffManager.Armor; } }
        public List<TagSynergy> Attack { get { return buffManager.Attack; } }

        public RoleModel (Actor owner, int strength, int willpower, int constitution, int speed, TagSynergy[] armor, int actionPointsIncome, List<Skill> skills,
            Skill attackingSkill)
        {
            this.owner = owner;
            this.strength = strength;
            this.willpower = willpower;
            this.constitution = constitution;
            this.speed = speed;
            this.armor = armor;
            this.actionPointsIncome = actionPointsIncome;
            this.skills = skills;
            this.buffManager = new BuffManager(this);
        }

        public RoleModel (Actor owner, RoleModelNative native)
        {
            this.owner = owner;
            this.strength = native.Strength;
            this.willpower = native.Willpower;
            this.constitution = native.Constitution;
            this.speed = native.Speed;
            this.armor = native.Armor;
            this.actionPointsIncome = native.ActionPointsIncome;
            this.skills = new List<Skill>();
            foreach(SkillNative skill in native.Skills)
            {
                skills.Add(new Skill(this, skill,null,null,null,null));
            }
            this.attackingSkill = new Skill(this, native.AttackSkill, 0, null, 1,null);
        }

        public Skill AddSkill (Skill skill)
        {
            owner.Affected = true;
            skills.Add(skill);
            return skill;
        }

        public Skill AddSkill (string native, float? cd, float? mod, int? cost, int? range)
        {
            return AddSkill(new Skill(this, owner.Parent.NativeManager.GetSkillNative(native), cd, mod, cost, range));
        }

        public Skill RemoveSkill (Skill skill)
        {
            owner.Affected = true;
            skills.Remove(skill);
            return skill;
        }

        public Skill RemoveSkill(uint id)
        {
            return RemoveSkill(skills.Find(x => x.Id == id));
        }

        public void SpendActionPoints (int amount)
        {
            this.actionPoints -= amount;
            if (actionPoints <= 0) owner.EndTurn();
        }

        public void Update (float time)
        {
            buffManager.Update(time);
            foreach(Skill skill in skills)
            {
                skill.Update(time);
            }
        }
    }
}
