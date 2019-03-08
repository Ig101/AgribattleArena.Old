using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Contexts.NativesEntities
{
    public class SceneAction
    {
        [Key]
        [MaxLength(30)]
        public string Id { get; set; }
        public byte[] Script { get; set; }
        public List<Decoration> Decorations { get; set; }
        [InverseProperty("BuffApplier")]
        public List<Buff> Buffs { get; set; }
        public List<Skill> Skills { get; set; }
        public List<SpecEffect> Effects { get; set; }
        public List<Tile> Tiles { get; set; }
    }
}
