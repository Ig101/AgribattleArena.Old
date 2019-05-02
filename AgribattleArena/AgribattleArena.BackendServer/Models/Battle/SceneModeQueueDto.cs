using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Battle
{
    public class SceneModeQueueDto
    {
        public List<ProfileQueueDto> Queue { get; set; }
        public SceneModeDto Mode { get; set; }
    }
}
