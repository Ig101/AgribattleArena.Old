using AgribattleArena.BackendServer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Hubs
{
    [Authorize]
    public class GameHub: Hub
    {
        ILogger<GameHub> _logger;
        IGameService _gameService;

        public GameHub(ILogger<GameHub> logger, IGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }

        public override Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            _gameService.ConnectedUsers.Add(userId);
            if (_gameService.ConnectedUsers.Where(x => x == userId).Count()>1)
            {
                Context.Abort();
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _gameService.ConnectedUsers.Remove(Context.UserIdentifier);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
