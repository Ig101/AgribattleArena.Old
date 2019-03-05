﻿using AgribattleArenaBackendServer.Engine.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Engine.NativeManager
{
    public interface INativeManager
    {
        TileNative GetTileNative(string id);
        ActorNative GetActorNative(string id);
        DecorationNative GetDecorationNative(string id);
        BuffNative GetBuffNative(string id);
        SkillNative GetSkillNative(string id);
        RoleModelNative GetRoleModelNative(string id);
        EffectNative GetEffectNative(string id);
    }
}
