using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Contexts.NativeEntities
{
    public class Tile
    {
        [Key]
        [MaxLength(30)]
        public string Id { get; set; }
        public List<Tag> Tags { get; set; }
        public bool Flat { get; set; }
        public int DefaultHeight { get; set; }
        public bool Unbearable { get; set; }
        public float Mod { get; set; }

        public int ActionId { get; set; }
        [ForeignKey("ActionId")]
        public SceneAction Action { get; set; }
        public int OnStepActionId { get; set; }
        [ForeignKey("OnStepActionId")]
        public SceneAction OnStepAction { get; set; }
    }
}
