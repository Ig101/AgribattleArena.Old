using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.DBProvider.Contexts.ProfileEntities
{
    public class Skill
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(30)]
        [Required]
        public string Native { get; set; }

        public long ActorId { get; set; }
        [ForeignKey("ActorId")]
        public Actor Actor { get; set; }
    }
}
