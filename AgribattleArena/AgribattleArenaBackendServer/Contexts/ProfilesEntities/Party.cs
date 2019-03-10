using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Contexts.ProfilesEntities
{
    public class Party
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public int ActorsLimit { get; set; }

        public List<Actor> Actors { get; set; }

        public string ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
    }
}
