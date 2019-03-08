using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgribattleArenaBackendServer.Contexts.NativesEntities
{
    public class Buff
    {
        [Key]
        [MaxLength(30)]
        public string Id { get; set; }

        public List<Tag> Tags { get; set; }
        public bool Repeatable { get; set; }
        public bool SummarizeLength { get; set; }
        public string ActionId { get; set; }
        [ForeignKey("ActionId")]
        public SceneAction Action { get; set; }
        public string BuffApplierId { get; set; }
        [ForeignKey("BuffApplierId")]
        public SceneAction BuffApplier { get; set; }
        public float? Duration { get; set; }
        public float Mod { get; set; }
    }
}
