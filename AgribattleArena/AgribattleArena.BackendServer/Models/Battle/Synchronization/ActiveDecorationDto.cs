﻿using AgribattleArena.BackendServer.Models.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Battle.Synchronization
{
    public class ActiveDecorationDto
    {
        public int Id { get; set; }
        public string NativeId { get; set; }
        public float Mod { get; set; }
        public float InitiativePosition { get; set; }
        public float Health { get; set; }
        public string OwnerId { get; set; }
        public bool IsAlive { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public float Z { get; set; }
        public float MaxHealth { get; set; }
        public IEnumerable<TagSynergyDto> Armor { get; set; }
    }
}
