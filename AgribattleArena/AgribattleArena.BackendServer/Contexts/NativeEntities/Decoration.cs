using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Contexts.NativeEntities
{
    public class Decoration
    {
        [Key]
        [MaxLength(30)]
        public string Id { get; set; }

        public List<Tag> Tags { get; set; }
        public int Health { get; set; }
        public float Z { get; set; }
        public float Mod { get; set; }
        public List<TagSynergy> Armor { get; set; }

        [InverseProperty("DecorationAction")]
        public List<SceneAction> Action { get; set; }
        [InverseProperty("DecorationOnDeathAction")]
        public List<SceneAction> OnDeathAction { get; set; }
    }
}
