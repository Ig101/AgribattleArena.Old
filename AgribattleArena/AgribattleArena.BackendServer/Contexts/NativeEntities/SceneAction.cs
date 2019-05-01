using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Contexts.NativeEntities
{
    public class SceneAction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }


        public string DecorationActionId { get; set; }
        [ForeignKey("DecorationActionId")]
        public Decoration DecorationAction { get; set; }
        public string DecorationOnDeathActionId { get; set; }
        [ForeignKey("DecorationOnDeathActionId")]
        public Decoration DecorationOnDeathAction { get; set; }

        public string BuffActionId { get; set; }
        [ForeignKey("BuffActionId")]
        public Buff BuffAction { get; set; }
        public string BuffApplierId { get; set; }
        [ForeignKey("BuffApplierId")]
        public Buff BuffApplier { get; set; }
        public string BuffOnPurgeActionId { get; set; }
        [ForeignKey("BuffOnPurgeActionId")]
        public Buff BuffOnPurgeAction { get; set; }

        public string SkillActionId { get; set; }
        [ForeignKey("SkillActionId")]
        public Skill SkillAction { get; set; }

        public string EffectActionId { get; set; }
        [ForeignKey("EffectActionId")]
        public SpecEffect EffectAction { get; set; }
        public string EffectOnDeathActionId { get; set; }
        [ForeignKey("EffectOnDeathActionId")]
        public SpecEffect EffectOnDeathAction { get; set; }

        public string TileActionId { get; set; }
        [ForeignKey("TileActionId")]
        public Tile TileAction { get; set; }
        public string TileOnStepActionId { get; set; }
        [ForeignKey("TileOnStepActionId")]
        public Tile TileOnStepAction { get; set; }
    }
}
