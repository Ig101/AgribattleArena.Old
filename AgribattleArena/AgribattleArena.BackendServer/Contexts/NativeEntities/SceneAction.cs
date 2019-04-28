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

        [InverseProperty("Action")]
        public List<Decoration> Decorations { get; set; }
        [InverseProperty("OnDeathAction")]
        public List<Decoration> OnDeathDecorations { get; set; }

        [InverseProperty("Action")]
        public List<Buff> Buffs { get; set; }
        [InverseProperty("BuffApplier")]
        public List<Buff> ApplierBuffs { get; set; }
        [InverseProperty("OnPurgeAction")]
        public List<Buff> PurgeBuffs { get; set; }

        public List<Skill> Skills { get; set; }

        [InverseProperty("Action")]
        public List<SpecEffect> Effects { get; set; }
        [InverseProperty("OnDeathAction")]
        public List<SpecEffect> OnDeathEffects { get; set; }

        [InverseProperty("Action")]
        public List<Tile> Tiles { get; set; }
        [InverseProperty("OnStepAction")]
        public List<Tile> OnStepTiles { get; set; }
    }
}
