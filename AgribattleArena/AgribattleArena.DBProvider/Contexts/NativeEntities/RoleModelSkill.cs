using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.DBProvider.Contexts.NativeEntities
{
    public class RoleModelSkill
    {
        [MaxLength(30)]
        public int RoleModelId { get; set; }
        [ForeignKey("RoleModelId")]
        public RoleModel RoleModel { get; set; }

        [MaxLength(30)]
        public int SkillId { get; set; }
        [ForeignKey("SkillId")]
        public Skill Skill { get; set; }
    }
}
