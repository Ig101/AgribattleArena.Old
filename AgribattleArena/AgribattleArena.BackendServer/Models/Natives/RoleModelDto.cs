using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Natives
{
    public class RoleModelDto
    {
        public string Id { get; set; }
        public int Strength { get; set; }
        public int Willpower { get; set; }
        public int Constitution { get; set; }
        public int Speed { get; set; }
        public int ActionPointsIncome { get; set; }
        public IEnumerable<SkillDto> RoleModelSkills { get; set; }
        public SkillDto AttackingSkill { get; set; }
    }
}
