using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Contexts.ProfilesEntities
{
    public class Skill
    {
        [Key]
        int Id { get; set; }
        [MaxLength(30)]
        [Required]
        string Native { get; set; }

        public int ActorId { get; set; }
        [ForeignKey("ActorId")]
        public Actor Player { get; set; }
    }
}
