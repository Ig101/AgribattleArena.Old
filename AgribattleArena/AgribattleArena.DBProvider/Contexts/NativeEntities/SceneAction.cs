using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.DBProvider.Contexts.NativeEntities
{
    public class SceneAction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }


        public int DecorationActionId { get; set; }
        [ForeignKey("DecorationActionId")]
        public Decoration DecorationAction { get; set; }
        public int DecorationOnDeathActionId { get; set; }
        [ForeignKey("DecorationOnDeathActionId")]
        public Decoration DecorationOnDeathAction { get; set; }

        public int BuffActionId { get; set; }
        [ForeignKey("BuffActionId")]
        public Buff BuffAction { get; set; }
        public int BuffApplierId { get; set; }
        [ForeignKey("BuffApplierId")]
        public Buff BuffApplier { get; set; }
        public int BuffOnPurgeActionId { get; set; }
        [ForeignKey("BuffOnPurgeActionId")]
        public Buff BuffOnPurgeAction { get; set; }

        public int SkillActionId { get; set; }
        [ForeignKey("SkillActionId")]
        public Skill SkillAction { get; set; }

        public int EffectActionId { get; set; }
        [ForeignKey("EffectActionId")]
        public SpecEffect EffectAction { get; set; }
        public int EffectOnDeathActionId { get; set; }
        [ForeignKey("EffectOnDeathActionId")]
        public SpecEffect EffectOnDeathAction { get; set; }

        public int TileActionId { get; set; }
        [ForeignKey("TileActionId")]
        public Tile TileAction { get; set; }
        public int TileOnStepActionId { get; set; }
        [ForeignKey("TileOnStepActionId")]
        public Tile TileOnStepAction { get; set; }
    }
}
