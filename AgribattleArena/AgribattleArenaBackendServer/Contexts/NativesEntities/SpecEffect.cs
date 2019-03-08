using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgribattleArenaBackendServer.Contexts.NativesEntities
{
    public class SpecEffect
    {
        [Key]
        [MaxLength(30)]
        public string Id { get; set; }

        public List<Tag> Tags { get; set; }
        public float DefaultZ { get; set; }
        public float DefaultDuration { get; set; }
        public float DefaultMod { get; set; }
        public string ActionId { get; set; }
        [ForeignKey("ActionId")]
        public SceneAction Action { get; set; }
    }
}
