using AgribattleArenaBackendServer.Models.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Models.Queueing
{
    public class SceneModeQueueDto
    {
        public List<BattleUserDto> Queue { get; set; }
        public SceneModeDto Mode { get; set; }
    }
}
