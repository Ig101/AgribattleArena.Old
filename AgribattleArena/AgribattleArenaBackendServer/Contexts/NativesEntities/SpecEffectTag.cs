using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Contexts.NativesEntities
{
    public class SpecEffectTag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
        [ForeignKey("ObjectId")]
        public SpecEffect Object { get; set; }
        public string ObjectId { get; set; }
    }
}
