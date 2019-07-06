using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.DBProvider.Contexts.NativeEntities
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }

        public int? DecorationId { get; set; }
        [ForeignKey("DecorationId")]
        public Decoration Decoration { get; set; }
        public int? ActorId { get; set; }
        [ForeignKey("ActorId")]    
        public Actor Actor { get; set; }
        public int? SpecEffectId { get; set; }
        [ForeignKey("SpecEffectId")]
        public SpecEffect SpecEffect { get; set; }
        public int? BuffId { get; set; }
        [ForeignKey("BuffId")]
        public Buff Buff { get; set; }
        public int? TileId { get; set; }
        [ForeignKey("TileId")]
        public Tile Tile { get; set; }
        public int? SkillId { get; set; }
        [ForeignKey("SkillId")]
        public Skill Skill { get; set; }
    }
}
