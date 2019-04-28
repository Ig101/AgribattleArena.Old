using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Contexts.StoreEntities
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        [Required]
        public string Native { get; set; }

        public int ActorId { get; set; }
        [ForeignKey("ActorId")]
        public Actor Actor { get; set; }
    }
}
