using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Contexts.ProfileEntities
{

    public class TagsArmor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string Tag { get; set; }
        public float Mod { get; set; }

        public int ActorId { get; set; }
        [ForeignKey("ActorId")]
        public Actor Actor { get; set; }
    }
}
