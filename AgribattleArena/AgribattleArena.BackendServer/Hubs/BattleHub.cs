using AgribattleArena.BackendServer.Services;
using AgribattleArena.BackendServer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Hubs
{
    [Authorize]
    public class BattleHub : Hub
    {
        IQueueService _queueService;

        public BattleHub(IQueueService queueService)
        {
            _queueService = queueService;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _queueService.Dequeue(Context.UserIdentifier);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
