using System;
using System.Collections.Generic;
using System.Text;
using AgribattleArena.Engine.Natives;

namespace AgribattleArena.Engine.NativeManagers
{
    class NativeManager : INativeManager, ForExternalUse.INativeManager
    {
        public ActorNative GetActorNative(string id)
        {
            throw new NotImplementedException();
        }

        public BuffNative GetBuffNative(string id)
        {
            throw new NotImplementedException();
        }

        public ActiveDecorationNative GetDecorationNative(string id)
        {
            throw new NotImplementedException();
        }

        public SpecEffectNative GetEffectNative(string id)
        {
            throw new NotImplementedException();
        }

        public RoleModelNative GetRoleModelNative(string id)
        {
            throw new NotImplementedException();
        }

        public SkillNative GetSkillNative(string id)
        {
            throw new NotImplementedException();
        }

        public TileNative GetTileNative(string id)
        {
            throw new NotImplementedException();
        }
    }
}
