using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Contexts.NativesEntities
{
    public class RoleModelTagSynergy
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(15)]
        public string SelfTag { get; set; }
        [MaxLength(15)]
        [Required]
        public string TargetTag { get; set; }
        public float Mod { get; set; }
        [ForeignKey("OwnerId")]
        public RoleModel Owner { get; set; }
        public string OwnerId { get; set; }
    }
}
