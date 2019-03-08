using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Contexts.NativesEntities
{
    public class Actor
    {
        [Key]
        [MaxLength(30)]
        public string Id { get; set; }

        public List<Tag> Tags { get; set; }
        public float DefaultZ { get; set; }
    }
}
