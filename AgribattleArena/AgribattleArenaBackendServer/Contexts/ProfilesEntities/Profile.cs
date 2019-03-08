using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Contexts.ProfilesEntities
{
    public class Profile
    {
        [Key]
        [MaxLength(20)]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<Player> Actors { get; set; }
    }
}
