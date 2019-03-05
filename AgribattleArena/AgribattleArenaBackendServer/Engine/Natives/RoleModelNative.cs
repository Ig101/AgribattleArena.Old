using AgribattleArenaBackendServer.Engine.ActorModel;
using AgribattleArenaBackendServer.Engine.NativeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.Natives
{
    public class RoleModelNative
    {
        int strength;
        int willpower;
        int constitution;
        int speed;
        int actionPointsIncome;
        TagSynergy[] armor;
        SkillNative[] skills;
        SkillNative attackSkill;

        public SkillNative AttackSkill { get { return attackSkill; } }
        public int Strength { get { return strength; } }
        public int Willpower { get { return willpower; } }
        public int Constitution { get { return constitution; } }
        public int Speed { get { return speed; } }
        public TagSynergy[] Armor { get { return armor; } }
        public SkillNative[] Skills { get { return skills; } }
        public int ActionPointsIncome { get { return actionPointsIncome; } }

        public RoleModelNative (int strength, int willpower, int constitution, int speed, TagSynergy[] armor, SkillNative attackSkill, SkillNative[] skills, int actionPointsIncome)
        {
            this.strength = strength;
            this.willpower = willpower;
            this.speed = speed;
            this.constitution = constitution;
            this.armor = armor;
            this.attackSkill = attackSkill;
            this.skills = skills;
            this.actionPointsIncome = actionPointsIncome;
        }

        public RoleModelNative(INativeManager nativeManager, int strength, int willpower, int constitution, int speed, TagSynergy[] armor, string attackSkillId, string[] skillsId, int actionPointsIncome)
        {
            this.strength = strength;
            this.willpower = willpower;
            this.speed = speed;
            this.constitution = constitution;
            this.armor = armor;
            this.attackSkill = nativeManager.GetSkillNative(attackSkillId);
            this.skills = new SkillNative[skillsId.Length];
            for(int i = 0; i<skills.Length;i++)
            {
                this.skills[i] = nativeManager.GetSkillNative(skillsId[i]);
            }
            this.actionPointsIncome = actionPointsIncome;
        }
    }
}
