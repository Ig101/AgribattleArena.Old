using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Models.Natives
{
    public class ActionNativeDto
    {
        [MaxLength(30)]
        [MinLength(3)]
        public string Name { get; set; }
        public string Script { get; set; }
    }
}
