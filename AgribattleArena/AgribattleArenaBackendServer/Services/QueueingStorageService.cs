using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Models.Battle;
using AgribattleArenaBackendServer.Models.Queueing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public class QueueingStorageService: IQueueingStorageService, IQueueingStorageServiceHubLink, IQueueingStorageServiceQueueLink
    {
        Dictionary<string, SceneModeQueueDto> queues;

        public QueueingStorageService()
        {
            queues = new Dictionary<string, SceneModeQueueDto>()
            {
                { "duel", new SceneModeQueueDto()
                    {
                        Queue = new List<BattleUserDto>(),
                        Mode = new SceneModeDto()
                        {
                            Generator = new DuelLevelGenerator(),
                            MaxPlayers = 2
                        }
                    }
                }
            };
        }

        public bool Enqueue(string mode, BattleUserDto user)
        {
            SceneModeQueueDto targetQueue;
            if((targetQueue = queues[mode]) !=null)
            {
                foreach(SceneModeQueueDto queue in queues.Values)
                {
                    if(queue.Queue.Find(x => x.UserId == user.UserId) != null)
                    {
                        return false;
                    }
                }
                targetQueue.Queue.Add(user);
                return true;
            }
            return false;
        }

        public bool Dequeue(string userId)
        {
            foreach(SceneModeQueueDto queue in queues.Values)
            {
                BattleUserDto user;
                if ((user = queue.Queue.Find(x => x.UserId == userId)) != null)
                {
                    queue.Queue.Remove(user);
                    return true;
                }
            }
            return false;
        }

        public List<SceneModeQueueDto> GetFullGroups()
        {
            List<SceneModeQueueDto> playersParties = new List<SceneModeQueueDto>();
            foreach(SceneModeQueueDto queue in queues.Values)
            {
                //TODO QueueProcessing

                //
                    
            }
            return playersParties;
        }
    }
}
