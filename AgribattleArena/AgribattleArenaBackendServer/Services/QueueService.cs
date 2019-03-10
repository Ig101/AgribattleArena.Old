using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Models.Battle;
using AgribattleArenaBackendServer.Models.Queueing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public class QueueService : IHostedService, IQueueService, IQueueStorageServiceHubLink
    {
        IEngineServiceQueueLink engineServer;
        IServiceScopeFactory serviceScopeFactory;
        Timer timer;
        Dictionary<string, SceneModeQueueDto> queues;

        public QueueService(IEngineServiceQueueLink engineServer, IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.engineServer = engineServer;
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

        //TODO Processing
        void QueueProcessing (object state)
        {
            List<SceneModeQueueDto> groups = new List<SceneModeQueueDto>();
            foreach (SceneModeQueueDto queue in queues.Values)
            {
                //TODO QueueProcessing

                //

            }
            foreach (SceneModeQueueDto group in groups)
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var scopedProcessingService =
                        scope.ServiceProvider
                            .GetRequiredService<IProfilesService>();
                    
                }
            }
        }

        public bool Enqueue(string mode, BattleUserDto user)
        {
            SceneModeQueueDto targetQueue;
            if ((targetQueue = queues[mode]) != null)
            {
                foreach (SceneModeQueueDto queue in queues.Values)
                {
                    if (queue.Queue.Find(x => x.UserId == user.UserId) != null)
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
            foreach (SceneModeQueueDto queue in queues.Values)
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

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(QueueProcessing, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
