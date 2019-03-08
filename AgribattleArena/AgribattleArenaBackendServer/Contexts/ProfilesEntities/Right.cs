using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Contexts.ProfilesEntities
{
    public class Right
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(70)]
        public string Name { get; set; }
        public List<RoleRight> RoleRights { get; set; }
    }
}
