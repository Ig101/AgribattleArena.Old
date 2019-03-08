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
        public int ActionId { get; set; }
        [ForeignKey("ActionId")]
        public Action Action { get; set; }
        public int BuffApplierId { get; set; }
        [ForeignKey("BuffApplierId")]
        public Action BuffApplier { get; set; }
        public float? Duration { get; set; }
        public float Mod { get; set; }
    }
}
