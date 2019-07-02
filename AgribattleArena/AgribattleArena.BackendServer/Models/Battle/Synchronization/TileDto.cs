﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Battle.Synchronization
{
    public class TileDto
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int? TempActorId { get; set; }
        public float Height { get; set; }
        public string NativeId { get; set; }
        public string OwnerId { get; set; }
    }
}
