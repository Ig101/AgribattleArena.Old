﻿using AgribattleArena.Engine.ForExternalUse.Synchronization.ObjectInterfaces;
using AgribattleArena.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgribattleArena.Engine.Synchronizers.SynchronizationObjects
{
    class Player: IPlayer, ForExternalUse.Generation.ObjectInterfaces.IPlayer
    {
        public List<ForExternalUse.Generation.ObjectInterfaces.IActor> KeyActorsGen { get; }

        public string Id { get; }
        public int? Team { get; }
        public List<int> KeyActorsSync { get; }
        public int TurnsSkipped { get; }
        public PlayerStatus Status { get; }

        public Player(Engine.Player player)
        {
            this.Id = player.Id;
            this.Team = player.Team;
            this.KeyActorsSync = player.KeyActors.Select(x => x.Id).ToList();
            this.TurnsSkipped = player.TurnsSkipped;
            this.Status = player.Status;
        }

        public Player(string id, int? team, List<ForExternalUse.Generation.ObjectInterfaces.IActor> keyActors)
        {
            this.Team = team;
            this.Id = id;
            this.KeyActorsGen = keyActors;
        }
    }
}
