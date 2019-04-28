using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Contexts.NativeEntities
{
    public class Skill
    {
        [Key]
        [MaxLength(30)]
        public string Id { get; set; }
        public List<Tag> Tags { get; set; }
        public int Range { get; set; }
        public int Cost { get; set; }
        public float Cd { get; set; }
        public float Mod { get; set; }

        public List<RoleModel> RoleModels { get; set; }
        public List<RoleModelSkill> RoleModelSkills { get; set; }

        public int ActionId { get; set; }
        [ForeignKey("ActionId")]
        public SceneAction Action { get; set; }
    }
}
