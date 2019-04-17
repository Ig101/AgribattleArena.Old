using AgribattleArena.Engine.ForExternalUse.Synchronization.ObjectInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Synchronizers.SynchronizationObjects
{
    class Player: IPlayer, ForExternalUse.Generation.ObjectInterfaces.IPlayer
    {

        public int Id => throw new NotImplementedException();

        public List<ForExternalUse.Generation.ObjectInterfaces.IActor> KeyActors => throw new NotImplementedException();

        public Player(Engine.Player player)
        {
        }
    }
}
