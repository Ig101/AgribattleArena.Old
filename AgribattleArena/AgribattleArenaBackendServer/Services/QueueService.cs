using AgribattleArenaBackendServer.Models.Queueing;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Services
{
    public class QueueService : IHostedService
    {
        IEngineServiceQueueLink engineServer;
        IQueueingStorageServiceQueueLink queueService;
        IBattleServiceQueueLink battleService;
        IProfilesServiceQueueLink profilesService;
        Timer timer;

        public QueueService(IEngineServiceQueueLink engineServer, IQueueingStorageServiceQueueLink queueService, IBattleServiceQueueLink battleService,
            IProfilesServiceQueueLink profilesService)
        {
            this.profilesService = profilesService;
            this.engineServer = engineServer;
            this.queueService = queueService;
            this.battleService = battleService;
        }

        //TODO Processing
        void QueueProcessing (object state)
        {
            List<SceneModeQueueDto> groups = queueService.GetFullGroups();
            foreach(SceneModeQueueDto group in groups)
            {

            }
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
