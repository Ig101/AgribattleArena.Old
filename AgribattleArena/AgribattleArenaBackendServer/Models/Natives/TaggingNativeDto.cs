using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Models.Natives
{
    public class TaggingNativeDto
    {
        [MaxLength(30)]
        [MinLength(2)]
        public string Id { get; set; }
        [MaxLength(15)]
        public List<string> Tags { get; set; }
    }
}
