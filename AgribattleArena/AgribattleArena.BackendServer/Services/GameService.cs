using AgribattleArena.BackendServer.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Services
{
    public class GameService: IGameService
    {
        ILogger<GameService> _logger;
        List<string> _connectedUsers;

        public List<string> ConnectedUsers { get { return _connectedUsers; } }

        public GameService(ILogger<GameService> logger)
        {
            _logger = logger;
            _connectedUsers = new List<string>();
        }
    }
}
