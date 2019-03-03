using AgribattleArenaBackendServer.Engine.ActorModel;
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
    }
}
