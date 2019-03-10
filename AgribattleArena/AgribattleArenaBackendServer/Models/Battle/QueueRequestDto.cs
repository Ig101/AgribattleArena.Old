using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Models.Battle
{
    public class QueueRequestDto
    {
        public int PartyId { get; set; }
        public string Mode { get; set; } = "duel";
    }
}
