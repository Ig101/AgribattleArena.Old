using AgribattleArena.BackendServer.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Services
{
    public class HostedBattleService: IHostedService
    {
        Timer _queueTimer;
        Timer _engineTimer;
        IBattleService _battleService;
        ILogger<HostedBattleService> _logger;

        public HostedBattleService(IBattleService battleService, ILogger<HostedBattleService> logger)
        {
            _battleService = battleService;
            _logger = logger;
        }

        void EngineProcessing(object state)
        {
            _logger.LogTrace("Engine time processing");
            _battleService.EngineTimeProcessing((int)state);
        }

        void QueueProcessing(object state)
        {
            _logger.LogTrace("Queue time processing");
            _battleService.QueueProcessing();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _queueTimer = new Timer(QueueProcessing, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(5));
            _engineTimer = new Timer(EngineProcessing, 5, TimeSpan.Zero,
            TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _queueTimer?.Change(Timeout.Infinite, 0);
            _engineTimer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _queueTimer?.Dispose();
            _engineTimer?.Dispose();
        }
    }
}
