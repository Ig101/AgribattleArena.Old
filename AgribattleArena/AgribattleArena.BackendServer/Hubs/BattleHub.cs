using AgribattleArena.BackendServer.Services;
using AgribattleArena.BackendServer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Hubs
{
    [Authorize]
    public class BattleHub : Hub
    {
        IBattleService _battleService;

        public BattleHub(IBattleService battleService, ILogger<BattleHub> logger)
        {
            _battleService = battleService;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _battleService.Dequeue(Context.UserIdentifier);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
