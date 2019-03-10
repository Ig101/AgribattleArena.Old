using AgribattleArenaBackendServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Hubs
{
    [Authorize]
    public class BattleHub: Hub
    {
        IEngineService engineService;
        IQueueingService queueingService;

        public BattleHub(IEngineService engineService, IQueueingService queueingService)
        {
            this.engineService = engineService;
            this.queueingService = queueingService;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
