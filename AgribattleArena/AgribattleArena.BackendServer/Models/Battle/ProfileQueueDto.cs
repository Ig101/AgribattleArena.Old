﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Battle
{
    public class ProfileQueueDto
    {
        public string ProfileId { get; set; }
        public int RevelationLevel { get; set; }
        public int Time { get; set; }
    }
}
