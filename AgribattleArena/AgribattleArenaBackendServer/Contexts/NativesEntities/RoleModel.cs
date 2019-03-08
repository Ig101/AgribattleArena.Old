using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Contexts.NativesEntities
{
    public class RoleModel
    {
        [Key]
        [MaxLength(30)]
        public string Id { get; set; }
        public int Strength { get; set; }
        public int Willpower { get; set; }
        public int Constitution { get; set; }
        public int Speed { get; set; }
        public int ActionPointsIncome { get; set; }
        [ForeignKey("AttackingSkillId")]
        public Skill AttackingSkill { get; set; }
        public string AttackingSkillId { get; set; }
        public List<TagSynergy> TagSynergies { get; set; }
        public List<RoleModelSkill> RoleModelSkills { get; set; }
    }
}
