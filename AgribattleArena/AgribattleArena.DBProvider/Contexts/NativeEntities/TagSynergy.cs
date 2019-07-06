﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.DBProvider.Contexts.NativeEntities
{
    public class TagSynergy
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(15)]
        public string SelfTag { get; set; }
        [MaxLength(15)]
        [Required]
        public string TargetTag { get; set; }
        public float Mod { get; set; }

        public int? RoleModelId { get; set; }
        [ForeignKey("RoleModelId")]
        public RoleModel RoleModel { get; set; }
        public int? DecorationId { get; set; }
        [ForeignKey("DecorationId")]
        public Decoration Decoration { get; set; }
    }
}
