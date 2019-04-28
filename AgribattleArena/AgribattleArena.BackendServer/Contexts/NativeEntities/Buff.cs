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

        public int ActionId { get; set; }
        [ForeignKey("ActionId")]
        public SceneAction Action { get; set; }
        public int BuffApplierId { get; set; }
        [ForeignKey("BuffApplierId")]
        public SceneAction BuffApplier { get; set; }
        public int OnPurgeActionId { get; set; }
        [ForeignKey("OnPurgeActionId")]
        public SceneAction OnPurgeAction { get; set; }
    }
}
