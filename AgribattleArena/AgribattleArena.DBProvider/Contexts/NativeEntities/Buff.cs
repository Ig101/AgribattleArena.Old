using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.DBProvider.Contexts.NativeEntities
{
    public class Buff
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public List<Tag> Tags { get; set; }
        public bool Eternal { get; set; }
        public int Repeatable { get; set; }
        public bool SummarizeLength { get; set; }
        public int? Duration { get; set; }
        public float Mod { get; set; }

        [InverseProperty("BuffAction")]
        public List<SceneAction> Action { get; set; }
        [InverseProperty("BuffApplier")]
        public List<SceneAction> BuffApplier { get; set; }
        [InverseProperty("BuffOnPurgeAction")]
        public List<SceneAction> OnPurgeAction { get; set; }
    }
}
