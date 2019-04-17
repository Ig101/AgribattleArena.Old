using AgribattleArena.Engine.ForExternalUse.Synchronization.ObjectInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Synchronizers.SynchronizationObjects
{
    class Actor: IActor, ForExternalUse.Generation.ObjectInterfaces.IActor
    {
        public int ExternalId => throw new NotImplementedException();

        public string NativeId => throw new NotImplementedException();

        public string AttackingSkillName => throw new NotImplementedException();

        public int Strength => throw new NotImplementedException();

        public int Willpower => throw new NotImplementedException();

        public int Constitution => throw new NotImplementedException();

        public int Speed => throw new NotImplementedException();

        public string[] SkillNames => throw new NotImplementedException();

        public int ActionPointsIncome => throw new NotImplementedException();

        public Actor(Objects.Actor actor)
        {
        }
    }
}
