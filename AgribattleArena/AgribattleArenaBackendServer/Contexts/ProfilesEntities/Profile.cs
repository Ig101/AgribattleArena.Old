﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int MaxPlayers { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<Player> Actors { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
