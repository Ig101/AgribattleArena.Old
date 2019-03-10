using AgribattleArenaBackendServer.Models.Queueing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public class QueueingService: IQueueingService
    {
        Dictionary<string, SceneModeQueueDto> queues;

        public QueueingService ()
        {
            queues = new Dictionary<string, SceneModeQueueDto>()
            {

            };
        }
    }
}
