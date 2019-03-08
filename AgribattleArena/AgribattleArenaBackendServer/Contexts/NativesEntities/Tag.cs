using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Contexts.NativesEntities
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
        [ForeignKey("DecorationId")]
        public Decoration Decoration { get; set; }
        public string DecorationId { get; set; }
        [ForeignKey("ActorId")]
        public Actor Actor { get; set; }
        public string ActorId { get; set; }
        [ForeignKey("SpecEffectId")]
        public SpecEffect SpecEffect { get; set; }
        public string SpecEffectId { get; set; }
        [ForeignKey("BuffId")]
        public Buff Buff { get; set; }
        public string BuffId { get; set; }
        [ForeignKey("TileId")]
        public Tile Tile { get; set; }
        public string TileId { get; set; }
        [ForeignKey("SkillId")]
        public Skill Skill { get; set; }
        public string SkillId { get; set; }
    }
}
