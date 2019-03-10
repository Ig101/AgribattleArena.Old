using AgribattleArenaBackendServer.Contexts;
using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Engine.NativeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    //TODO Not Ready
    public class BattleService: IBattleServiceQueueLink
    {
        BattleContext context;

        public BattleService(BattleContext context)
        {
            this.context = context;
        }

        public int AddNewBattle(List<int> players, int seed)
        {
            return 1;
        }
    }
}
