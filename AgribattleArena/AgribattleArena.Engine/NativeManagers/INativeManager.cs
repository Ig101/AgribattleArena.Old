using AgribattleArena.Engine.Natives;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.NativeManagers
{
    interface INativeManager
    {
        TileNative GetTileNative(string id);
        ActorNative GetActorNative(string id);
        ActiveDecorationNative GetDecorationNative(string id);
        BuffNative GetBuffNative(string id);
        SkillNative GetSkillNative(string id);
        RoleModelNative GetRoleModelNative(string id);
        SpecEffectNative GetEffectNative(string id);
    }
}
