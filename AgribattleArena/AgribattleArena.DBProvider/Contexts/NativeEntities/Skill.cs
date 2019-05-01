using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.DBProvider.Contexts.NativeEntities
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public List<Tag> Tags { get; set; }
        public int Range { get; set; }
        public int Cost { get; set; }
        public float Cd { get; set; }
        public float Mod { get; set; }

        public List<RoleModel> RoleModels { get; set; }
        public List<RoleModelSkill> RoleModelSkills { get; set; }

        public List<SceneAction> Action { get; set; }
    }
}
