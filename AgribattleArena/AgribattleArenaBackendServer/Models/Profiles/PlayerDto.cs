﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Models.Profiles
{
    public class PlayerDto
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public int ActorsLimit { get; set; }
        public int Victories { get; set; }
        public int Games { get; set; }
        public List<PlayerActorDto> Actors { get; set; }
    }
}
