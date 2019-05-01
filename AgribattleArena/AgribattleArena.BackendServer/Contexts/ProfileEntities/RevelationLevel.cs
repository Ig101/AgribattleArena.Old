using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Contexts.ProfileEntities
{
    public class RevelationLevel
    {
        [Key]
        public int Level { get; set; } 
        public int Revelations { get; set; }
    }
}
