using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Models.Profiles
{
    public class TagsArmorDto
    {
        [MaxLength(30)]
        [Required]
        public string Tag { get; set; }
        public float Mod { get; set; }
    }
}
