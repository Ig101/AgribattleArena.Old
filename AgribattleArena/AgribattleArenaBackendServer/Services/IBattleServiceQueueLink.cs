using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public interface IBattleServiceQueueLink
    {
        int AddNewBattle(List<int> players, int seed);
    }
}
