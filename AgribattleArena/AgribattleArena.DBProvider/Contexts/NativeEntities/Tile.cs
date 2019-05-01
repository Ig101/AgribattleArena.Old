using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.DBProvider.Contexts.NativeEntities
{
    public class Tile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public List<Tag> Tags { get; set; }
        public bool Flat { get; set; }
        public int DefaultHeight { get; set; }
        public bool Unbearable { get; set; }
        public float Mod { get; set; }

        [InverseProperty("TileAction")]
        public List<SceneAction> Action { get; set; }
        [InverseProperty("TileOnStepAction")]
        public List<SceneAction> OnStepAction { get; set; }
    }
}
