﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Contexts.ProfilesEntities
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public int ActorsLimit { get; set; }

        public int Victories { get; set; }
        public int Games { get; set; }
        public List<Actor> Actors { get; set; }

        public string ProfileLogin { get; set; }
        [ForeignKey("ProfileLogin")]
        public Profile Profile { get; set; }
    }
}
