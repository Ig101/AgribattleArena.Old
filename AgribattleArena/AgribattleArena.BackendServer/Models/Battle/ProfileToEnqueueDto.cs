﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Battle
{
    public class ProfileToEnqueueDto
    {
        public string ProfileId { get; set; }
        public int Revelations { get; set; }
        public string Mode { get; set; }
    }
}
