﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public interface IBattlefieldService
    {
        int AddNewBattle(List<int> players, int seed);
    }
}
