using AgribattleArena.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Models.Battle.Synchronization
{
    public class PlayerDto
    {
        public string Id { get; set; }
        public int? Team { get; set; }
        public IEnumerable<int> KeyActorsSync { get; set; }
        public int TurnsSkipped { get; set; }
        public PlayerStatus Status { get; set; }
    }
}
