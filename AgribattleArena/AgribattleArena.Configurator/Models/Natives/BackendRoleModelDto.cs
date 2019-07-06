using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Configurator.Models.Natives
{
    class BackendRoleModelDto: ActionObject
    {
        public string Name { get; set; }
        public int? Strength { get; set; }
        public int? Willpower { get; set; }
        public int? Constitution { get; set; }
        public int? Speed { get; set; }
        public int? ActionPointsIncome { get; set; }
        public List<string> RoleModelSkills { get; set; }
        public string AttackingSkill { get; set; }
    }
}
