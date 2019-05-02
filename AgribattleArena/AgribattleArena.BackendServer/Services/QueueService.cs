using AgribattleArena.BackendServer.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Services
{
    public class QueueService : IHostedService, IQueueService
    {
        Timer _timer;

        public QueueService ()
        {

        }

        void QueueProcessing(object state)
        {
            
        }

        public bool Enqueue(string mode, string profileId)
        {
            return false;
        }

        public bool Dequeue(string profileId)
        {
            return false;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(QueueProcessing, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
