using AgribattleArenaBackendServer.Engine.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Models.Queueing
{
    public class SceneModeDto
    {
        public int MaxPlayers { get; set; }
        public ILevelGenerator Generator { get; set; }
    }
}
