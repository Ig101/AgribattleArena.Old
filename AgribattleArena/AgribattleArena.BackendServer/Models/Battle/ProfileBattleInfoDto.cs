using AgribattleArena.BackendServer.Models.Battle.Synchronization;
using AgribattleArena.BackendServer.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Battle
{
    public class ProfileBattleInfoDto
    {
        
        public ProfileBattleStatus Status { get; set; }
        public SynchronizerDto BattleInfo { get; set; }
        public ProfileQueueInfoDto QueueInfo { get; set; }
    }
}
