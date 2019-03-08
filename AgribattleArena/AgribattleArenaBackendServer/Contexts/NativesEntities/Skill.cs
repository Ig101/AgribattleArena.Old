using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgribattleArenaBackendServer.Contexts.NativesEntities
{
    public class Skill
    {
        [Key]
        [MaxLength(30)]
        public string Id { get; set; }

        public List<Tag> Tags { get; set; }
        public int DefaultRange { get; set; }
        public int DefaultCost { get; set; }
        public float DefaultCd { get; set; }
        public float DefaultMod { get; set; }
        public int ActionId { get; set; }
        [ForeignKey("ActionId")]
        public Action Action { get; set; }
        public List<RoleModel> RoleModels { get; set; }
        public List<RoleModelSkill> RoleModelSkills { get; set; }
    }
}
