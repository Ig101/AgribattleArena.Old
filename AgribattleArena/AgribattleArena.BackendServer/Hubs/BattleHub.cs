using AgribattleArena.BackendServer.Services;
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
    public class BattleHub : Hub
    {
        IBattleService _battleService;
        ILogger<BattleHub> _logger;

        public BattleHub(IBattleService battleService, ILogger<BattleHub> logger)
        {
            _battleService = battleService;
            _logger = logger;
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

        public async void SendSynchronizationError(string profileId)
        {
            await this.Clients.User(profileId).SendAsync("BattleSynchronizationError");
        }

        public void OrderAttack(int actorId, int targetX, int targetY)
        {
            var profileId = Context.UserIdentifier;
            var scene = _battleService.GetUserScene(profileId);
            bool result = false;
            if (scene.GetPlayerActors(profileId).Contains(actorId))
            {
                scene.ActorAttack(actorId, targetX, targetY);
            }
            if (!result)
            {
                SendSynchronizationError(profileId);
            }
        }

        public void OrderMove(int actorId, int targetX, int targetY)
        {
            var profileId = Context.UserIdentifier;
            var scene = _battleService.GetUserScene(profileId);
            bool result = false;
            if (scene.GetPlayerActors(profileId).Contains(actorId))
            {
                scene.ActorMove(actorId, targetX, targetY);
            }
            if (!result)
            {
                SendSynchronizationError(profileId);
            }
        }

        public void OrderCast(int actorId, int skillId, int targetX, int targetY)
        {
            var profileId = Context.UserIdentifier;
            var scene = _battleService.GetUserScene(profileId);
            bool result = false;
            if (scene.GetPlayerActors(profileId).Contains(actorId))
            {
                scene.ActorCast(actorId, skillId, targetX, targetY);
            }
            if (!result)
            {
                SendSynchronizationError(profileId);
            }
        }

        public void OrderWait(int actorId)
        {
            var profileId = Context.UserIdentifier;
            var scene = _battleService.GetUserScene(profileId);
            bool result = false;
            if (scene.GetPlayerActors(profileId).Contains(actorId))
            {
                scene.ActorWait(actorId);
            }
            if (!result)
            {
                SendSynchronizationError(profileId);
            }
        }
    }
}
