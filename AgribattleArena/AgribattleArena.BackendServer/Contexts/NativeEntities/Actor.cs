using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Contexts.NativeEntities
{
    public class Actor
    {
        [Key]
        [MaxLength(30)]
        public string Id { get; set; }

        public List<Tag> Tags { get; set; }
        public float DefaultZ { get; set; }
        public List<TagSynergy> Armor { get; set; }
    }
}
