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
        IEngineServiceHubLink engineService;
        IQueueStorageServiceHubLink queueingService;

        public BattleHub(IEngineServiceHubLink engineService, IQueueStorageServiceHubLink queueingService)
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
            queueingService.Dequeue(Context.UserIdentifier);
            engineService.LeaveScene(Context.UserIdentifier);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
