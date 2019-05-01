using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Contexts.NativeEntities
{
    public class Buff
    {
        [Key]
        [MaxLength(30)]
        public string Id { get; set; }
        public List<Tag> Tags { get; set; }
        public bool Eternal { get; set; }
        public bool Repeatable { get; set; }
        public bool SummarizeLength { get; set; }
        public float? Duration { get; set; }
        public float Mod { get; set; }

        [InverseProperty("BuffAction")]
        public List<SceneAction> Action { get; set; }
        [InverseProperty("BuffApplier")]
        public List<SceneAction> BuffApplier { get; set; }
        [InverseProperty("BuffOnPurgeAction")]
        public List<SceneAction> OnPurgeAction { get; set; }
    }
}
